using AutoMapper;
using JobScheduler.Database.Context;
using JobScheduler.Interfaces;
using JobScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController: ControllerBase
    {
        private readonly JobSchedulerContext _context;
        private readonly ILogger<JobsController> _logger;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ISort _sortService;
        private readonly IMapper _mapper;

        public JobsController(JobSchedulerContext context, ILogger<JobsController> logger,
            IBackgroundTaskQueue taskQueue,
        ISort sortService,
        IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _taskQueue = taskQueue;
            _sortService = sortService;
            _mapper = mapper;
        }
        /// <summary>
        /// Post: Post the Input array
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostJob")]
        public IActionResult Post(JobRequest payload)
        {
            try
            {
                if (payload == null || payload.Input.Count == 0)
                    return StatusCode(422, "Invalid payload");

                Guid jobId = Guid.NewGuid();

                _context.JobsManager.Add(new Database.Entity.JobsManager
                {
                    JobId = jobId.ToString(),
                    JobItem = JsonConvert.SerializeObject(payload.Input),
                    ReceivedTime = DateTime.UtcNow,
                    Status = JobStatus.Initiated
                });

                _context.SaveChangesAsync();

                var jobResponse = new JobResponse()
                {
                    JobId = jobId.ToString(),
                    ReceivedTime = DateTime.UtcNow,
                    Status = JobStatus.Initiated,
                    Url= $"https://localhost:44365/Jobs/GetJob?jobId={jobId.ToString()}",
                    Message = $"A seperate task in created to sort thr input array Use /Jobs/GetJob endpoint with JobId to get job details"
                };
                // Enqueue a background work item
                _taskQueue.QueueBackgroundWorkItem(async token =>  _sortService.SortIntsAsync(jobId.ToString()));
               
                return  Accepted(jobResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Get all pending task
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetJobs")]
        public IActionResult Get()
        {
            try
            {
                var jobs = _context.JobsManager.ToList();

                if (jobs.Count == 0)
                    return NotFound("No Jobs found");

                var result =_mapper.Map<List<JobsManagerDTO>>(jobs);

               
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get the task by id
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetJob")]
        public IActionResult Get(string jobId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jobId))
                    return BadRequest("JobId is required");

                var job = _context.JobsManager.FirstOrDefault(x => x.JobId == jobId);

                if (job == null)
                    return NotFound("No Jobs found");

                var result = _mapper.Map<JobsManagerDTO>(job);

                return Ok(result);
            }       
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
