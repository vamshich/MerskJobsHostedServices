using System.Collections.Generic;

namespace JobScheduler.Models
{
    public class JobRequest
    {
        /// <summary>
        /// Int Items to sort
        /// </summary>
        public List<int> Input { get; set; }
    }

    public static class JobStatus
    {
        public static string Initiated = "Initiated";

        public static string Processing = "Processing";

        public static string Completed = "Completed";

        public static string Failed = "Failed";
    }
}
