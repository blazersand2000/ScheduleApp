using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   public class Schedule
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public ICollection<Shift> Shifts { get; set; }
   }
}
