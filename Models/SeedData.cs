using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ScheduleApp.Models
{
   public static class SeedData
   {
      public static void Initialize(IServiceProvider serviceProvider)
      {
         //using (var context = new ScheduleAppContext(
         //    serviceProvider.GetRequiredService<
         //        DbContextOptions<ScheduleAppContext>>()))
         //{
         //   // Look for existing.
         //   if (context.???.Any())
         //   {
         //      return;   // DB has been seeded
         //   }

         //   context.???.AddRange(
         //   );
         //   context.SaveChanges();
         //}
      }
   }
}