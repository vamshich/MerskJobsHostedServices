using System;

namespace JobScheduler.Models
{
    public class JobResponse
    {
        public string JobId { get; set; }

        public DateTime ReceivedTime { get; set; }
         
        public String Status { get; set; }

        public String Message { get; set; }

        public String Url { get; set; }
        
    }
}
