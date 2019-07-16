using Microsoft.EntityFrameworkCore;
using ScheduleApp.Models;

namespace ScheduleApp.Models
{
   public class ScheduleAppContext : DbContext
    {
        public ScheduleAppContext (DbContextOptions<ScheduleAppContext> options)
            : base(options)
        {
        }

        public DbSet<ScheduleApp.Models.Employee> Employee { get; set; }

        public DbSet<ScheduleApp.Models.Schedule> Schedule { get; set; }

        public DbSet<ScheduleApp.Models.Shift> Shift { get; set; }
    }
}
