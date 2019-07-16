using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   public class MultipleShiftsViewModel
   {
      [Display(Name = "Starting Date")]
      [DataType(DataType.Date)]
      public DateTime StartDate { get; set; }

      [Display(Name = "Ending Date")]
      [DataType(DataType.Date)]
      public DateTime EndDate { get; set; }

      [Display(Name = "Sunday")]
      public bool WorksSunday { get; set; }

      [Display(Name = "Monday")]
      public bool WorksMonday { get; set; }

      [Display(Name = "Tuesday")]
      public bool WorksTuesday { get; set; }

      [Display(Name = "Wednesday")]
      public bool WorksWednesday { get; set; }

      [Display(Name = "Thursday")]
      public bool WorksThursday { get; set; }

      [Display(Name = "Friday")]
      public bool WorksFriday { get; set; }

      [Display(Name = "Saturday")]
      public bool WorksSaturday { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? SundayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? SundayEndTime { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? MondayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? MondayEndTime { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? TuesdayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? TuesdayEndTime { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? WednesdayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? WednesdayEndTime { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? ThursdayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? ThursdayEndTime { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? FridayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? FridayEndTime { get; set; }

      [Display(Name = "Shift Start")]
      [DataType(DataType.Time)]
      public DateTime? SaturdayStartTime { get; set; }

      [Display(Name = "Shift End")]
      [DataType(DataType.Time)]
      public DateTime? SaturdayEndTime { get; set; }

      [Display(Name = "Employee")]
      public int EmployeeId { get; set; }

      [Display(Name = "Schedule")]
      public int ScheduleId { get; set; }

      public Employee Employee { get; set; }

      public Schedule Schedule { get; set; }

      public List<Shift> GetShifts()
      {
         List<Shift> shifts = new List<Shift>();
         Dictionary<DayOfWeek, dynamic> times = new Dictionary<DayOfWeek, dynamic>();

         //populate a dictionary of the days of week worked and the shift times
         if (WorksSunday && SundayStartTime != null && SundayStartTime != DateTime.MinValue && SundayEndTime != null && SundayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Sunday] = new { Start = SundayStartTime, End = SundayEndTime };
         }
         if (WorksMonday && MondayStartTime != null && MondayStartTime != DateTime.MinValue && MondayEndTime != null && MondayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Monday] = new { Start = MondayStartTime, End = MondayEndTime };
         }
         if (WorksTuesday && TuesdayStartTime != null && TuesdayStartTime != DateTime.MinValue && TuesdayEndTime != null && TuesdayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Tuesday] = new { Start = TuesdayStartTime, End = TuesdayEndTime };
         }
         if (WorksWednesday && WednesdayStartTime != null && WednesdayStartTime != DateTime.MinValue && WednesdayEndTime != null && WednesdayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Wednesday] = new { Start = WednesdayStartTime, End = WednesdayEndTime };
         }
         if (WorksThursday && ThursdayStartTime != null && ThursdayStartTime != DateTime.MinValue && ThursdayEndTime != null && ThursdayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Thursday] = new { Start = ThursdayStartTime, End = ThursdayEndTime };
         }
         if (WorksFriday && FridayStartTime != null && FridayStartTime != DateTime.MinValue && FridayEndTime != null && FridayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Friday] = new { Start = FridayStartTime, End = FridayEndTime };
         }
         if (WorksSaturday && SaturdayStartTime != null && SaturdayStartTime != DateTime.MinValue && SaturdayEndTime != null && SaturdayEndTime != DateTime.MaxValue)
         {
            times[DayOfWeek.Saturday] = new { Start = SaturdayStartTime, End = SaturdayEndTime };
         }

         //Create the shifts
         DateTime currentDate = StartDate.Date;
         while (currentDate <= EndDate.Date)
         {
            if (times.ContainsKey(currentDate.DayOfWeek))
            {
               shifts.Add(new Shift()
               {
                  EmployeeId = this.EmployeeId,
                  ScheduleId = this.ScheduleId,
                  Start = currentDate.Date + times[currentDate.DayOfWeek].Start.TimeOfDay,
                  End = currentDate.Date + times[currentDate.DayOfWeek].End.TimeOfDay
               });
            }
            currentDate = currentDate.AddDays(1).Date;
         }

         return shifts;
      }
   }
}
