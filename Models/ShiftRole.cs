using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   [Display(Name = "Shift Role")]
   public class ShiftRole
   {
      public int Id { get; set; }

      [Required]
      [StringLength(50)]
      public string Name { get; set; }

      [Display(Name = "Schedule")]
      public int ScheduleId { get; set; }

      public Schedule Schedule { get; set; }

   }
}
