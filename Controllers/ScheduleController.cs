using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleApp.Helpers;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
   public class ScheduleController : Controller
   {
      private readonly ScheduleAppContext _context;

      public ScheduleController(ScheduleAppContext context)
      {
         _context = context;
      }

      // GET: Schedule
      public async Task<IActionResult> Index()
      {
         return View(await _context.Schedule.ToListAsync());
      }

      // GET: Schedule/Details/5
      public async Task<IActionResult> Details(int? id, DateTime? SelectedWeekStartDay)
      {
         if (id == null)
         {
            return NotFound();
         }

         var schedule = await _context.Schedule
            .Include(sched => sched.Shifts).ThenInclude(shift => shift.Employee)
            .Include(sched => sched.Shifts).ThenInclude(shift => shift.ShiftRole)
            .FirstOrDefaultAsync(m => m.Id == id);
         if (schedule == null)
         {
            return NotFound();
         }

         var weeksWithShifts = (from shift in _context.Shift
                               select (shift.Start.FirstDayOfWeek().Date)).Concat(new List<DateTime>() { DateTime.Today.FirstDayOfWeek().Date }).Distinct().OrderBy(x => x.Date).ToListAsync().Result;
         var selectedWeek = SelectedWeekStartDay ?? weeksWithShifts.Where(x => x.Date <= DateTime.Today.Date).Last();
         var weeks = new SelectList(weeksWithShifts.Select(x => new SelectListItem($"{x.ToShortDateString()} - {x.AddDays(6).ToShortDateString()}", x.ToString())), "Value", "Text", selectedWeek);
         var weeklySchedule = WeeklyScheduleViewModel.Create(weeks, selectedWeek, schedule, await _context.Employee.Include(emp => emp.Shifts).ToListAsync());
            
         return View(weeklySchedule);
      }

      // GET: Schedule/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: Schedule/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Name")] Schedule schedule)
      {
         if (ModelState.IsValid)
         {
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(schedule);
      }

      // GET: Schedule/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var schedule = await _context.Schedule.FindAsync(id);
         if (schedule == null)
         {
            return NotFound();
         }
         return View(schedule);
      }

      // POST: Schedule/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Schedule schedule)
      {
         if (id != schedule.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(schedule);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ScheduleExists(schedule.Id))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(schedule);
      }

      // GET: Schedule/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var schedule = await _context.Schedule
             .FirstOrDefaultAsync(m => m.Id == id);
         if (schedule == null)
         {
            return NotFound();
         }

         return View(schedule);
      }

      // POST: Schedule/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var schedule = await _context.Schedule.FindAsync(id);
         _context.Schedule.Remove(schedule);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ScheduleExists(int id)
      {
         return _context.Schedule.Any(e => e.Id == id);
      }
   }
}
