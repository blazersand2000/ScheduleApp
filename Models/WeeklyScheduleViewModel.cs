using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   public class WeeklyScheduleViewModel
   {
      public SelectList Weeks { get; set; }
      public DateTime SelectedWeekStartDay { get; set; }
      //public DayOfWeek WeekStartsOn { get; set; }
      public int ScheduleId { get; set; }
      public Schedule Schedule { get; set; }
      public Dictionary<Employee, Dictionary<DateTime, List<Shift>>> EmpWkShifts { get; set; }

      public static WeeklyScheduleViewModel Create(SelectList weeks, DateTime selectedWeekStartDay, Schedule schedule, List<Employee> employees)
      {
         WeeklyScheduleViewModel weeklySchedule = new WeeklyScheduleViewModel()
         {
            Weeks = weeks,
            SelectedWeekStartDay = selectedWeekStartDay,
            ScheduleId = schedule.Id,
            Schedule = schedule,
            EmpWkShifts = new Dictionary<Employee, Dictionary<DateTime, List<Shift>>>()
         };

         foreach (var employee in employees)
         {
            var empShifts = new Dictionary<DateTime, List<Shift>>();
            for (var date = selectedWeekStartDay; date < selectedWeekStartDay.AddDays(7); date = date.AddDays(1))
            {
               var empDayShifts = new List<Shift>();
               empDayShifts.AddRange(employee.Shifts.Where(x => x.Start.Date == date.Date));
               empShifts[date] = empDayShifts;
            }
            weeklySchedule.EmpWkShifts[employee] = empShifts;
         }

         return weeklySchedule;
      }
   }
}
