using JobScheduler.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Database.Context
{
    public class JobSchedulerContext :DbContext
    {
        public JobSchedulerContext(DbContextOptions<JobSchedulerContext> options)
            : base(options)
        {
        }

        public DbSet<JobsManager> JobsManager { get; set; }

    }
}
