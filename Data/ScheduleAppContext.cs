using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ScheduleApp.Models
{
   public class ScheduleAppContext : IdentityDbContext<AppUser, IdentityRole, string>
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
