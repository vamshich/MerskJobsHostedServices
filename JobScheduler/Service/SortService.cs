using JobScheduler.Database.Context;
using JobScheduler.Interfaces;
using JobScheduler.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScheduler.Service
{
    public class SortService : ISort
    {
        private  JobSchedulerContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SortService> _logger;
        public  SortService(IServiceScopeFactory serviceScopeFactory, ILogger<SortService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
    }

        public async Task SortIntsAsync(string jobId)
        {
     
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                 _context = scope.ServiceProvider.GetService<JobSchedulerContext>();

                try
                {
                    var workItem = _context.JobsManager.FirstOrDefault(x => x.JobId == jobId.ToString());
                    workItem.Status = JobStatus.Processing;

                    //Just to mock long running task
                    await Task.Delay(TimeSpan.FromSeconds(30));

                    if (workItem == null)
                        return;

                    var inputArray = JsonConvert.DeserializeObject<List<int>>(workItem.JobItem);
                    inputArray.Sort();

                    workItem.Status = JobStatus.Completed;
                    workItem.CompletedTime = DateTime.UtcNow;
                    workItem.JobResult = JsonConvert.SerializeObject(inputArray);

                    _context.JobsManager.Update(workItem);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
    
        }
    }
}
