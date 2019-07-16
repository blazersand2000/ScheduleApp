using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Helpers
{
   public static class ExtensionMethods
   {
      public static DateTime FirstDayOfWeek(this DateTime dt)
      {
         return dt.AddDays(-1d * (double)dt.DayOfWeek);
      }
   }
}
