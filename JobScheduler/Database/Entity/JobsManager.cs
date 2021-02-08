using JobScheduler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobScheduler.Database.Entity
{
    public class JobsManager
    {
        [Key]
        public string JobId { get; set; }

        public string JobItem { get; set; }

        public DateTime? ReceivedTime { get; set; }

        public DateTime? CompletedTime { get; set; }

        public string Status { get; set; }

        public string JobResult { get; set; }
    }
}
