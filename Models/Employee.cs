using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   public class Employee
   {
      public int Id { get; set; }

      public string First { get; set; }

      public string Last { get; set; }

      public string Name
      {
         get
         {
            return $"{First} {Last}";
         }
      }

      public ICollection<Shift> Shifts { get; set; }
   }
}
