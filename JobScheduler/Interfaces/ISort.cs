using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScheduler.Interfaces
{
    public interface ISort
    {
        public  Task SortIntsAsync(string jobId);
    }
}
