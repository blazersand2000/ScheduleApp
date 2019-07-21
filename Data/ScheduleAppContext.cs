using Microsoft.EntityFrameworkCore;

namespace ScheduleApp.Models
{
   public class ScheduleAppContext : DbContext
    {
        public ScheduleAppContext (DbContextOptions<ScheduleAppContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<Shift> Shift { get; set; }

        public DbSet<ShiftRole> ShiftRole { get; set; }
    }
}
