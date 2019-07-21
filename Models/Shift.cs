using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   public class Shift
   {
      public int Id { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime Start { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime End { get; set; }

      public TimeSpan Duration { get => End - Start; }

      [Display(Name = "Employee")]
      public int EmployeeId { get; set; }

      public Employee Employee { get; set; }

      [Display(Name = "Schedule")]
      public int ScheduleId { get; set; }

      public Schedule Schedule { get; set; }

      [Display(Name = "Role")]
      public int? ShiftRoleId { get; set; }

      public ShiftRole ShiftRole { get; set; }
   }
}
