using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScheduler.Models
{
    public class JobsManagerDTO
    {       
        public string JobId { get; set; }

        public string JobItem { get; set; }

        public DateTime? ReceivedTime { get; set; }

        public DateTime? CompletedTime { get; set; }

        public string TimeTakenToProcess { get; set; }

        public string Status { get; set; }

        public string JobResult { get; set; }
    }
}
