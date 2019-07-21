using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
   public class ShiftController : Controller
   {
      private readonly ScheduleAppContext _context;

      public ShiftController(ScheduleAppContext context)
      {
         _context = context;
      }

      // GET: Shift
      public async Task<IActionResult> Index()
      {
         var ScheduleAppContext = _context.Shift.Include(s => s.Employee).Include(s => s.Schedule).Include(s => s.ShiftRole);
         return View(await ScheduleAppContext.ToListAsync());
      }

      // GET: Shift/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var shift = await _context.Shift
             .Include(s => s.Employee)
             .Include(s => s.Schedule)
             .Include(s => s.ShiftRole)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (shift == null)
         {
            return NotFound();
         }

         return View(shift);
      }

      // GET: Shift/Create
      public IActionResult Create()
      {
         ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name");
         ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name");
         return View();
      }

      // POST: Shift/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Start,End,EmployeeId,ScheduleId,ShiftRoleId")] Shift shift)
      {
         if (ModelState.IsValid)
         {
            _context.Add(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", shift.EmployeeId);
         ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name", shift.ScheduleId);
         ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRole.Where(r => r.ScheduleId == shift.ScheduleId), "Id", "Name", shift.ShiftRoleId);
         return View(shift);
      }

      // GET: Shift/CreateMultiple
      public IActionResult CreateMultiple()
      {
         ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name");
         ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name");
         return View(new MultipleShiftsViewModel()
         {
            StartDate = DateTime.Today,
            WorksSunday = true,
            WorksMonday = true,
            WorksTuesday = true,
            WorksWednesday = true,
            WorksThursday = true,
            WorksFriday = true,
            WorksSaturday = true,
         });
      }

      // POST: Shift/CreateMultiple
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> CreateMultiple(
         [Bind("StartDate,EndDate,WorksSunday,WorksMonday,WorksTuesday,WorksWednesday,WorksThursday,WorksFriday,WorksSaturday,SundayStartTime,SundayEndTime,MondayStartTime,MondayEndTime,TuesdayStartTime,TuesdayEndTime,WednesdayStartTime,WednesdayEndTime,ThursdayStartTime,ThursdayEndTime,FridayStartTime,FridayEndTime,SaturdayStartTime,SaturdayEndTime,EmployeeId,ScheduleId,Employee,Schedule")] MultipleShiftsViewModel shifts)
      {


         if (ModelState.IsValid)
         {
            _context.AddRange(shifts.GetShifts());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name");//, shift.EmployeeId);
         ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name");//, shift.ScheduleId);
         return View(shifts);
      }

      // GET: Shift/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var shift = await _context.Shift.FindAsync(id);
         if (shift == null)
         {
            return NotFound();
         }
         ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", shift.EmployeeId);
         ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name", shift.ScheduleId);
         ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRole.Where(r => r.ScheduleId == shift.ScheduleId), "Id", "Name", shift.ShiftRoleId);
         return View(shift);
      }

      // POST: Shift/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,EmployeeId,ScheduleId,ShiftRoleId")] Shift shift)
      {
         if (id != shift.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(shift);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ShiftExists(shift.Id))
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
         ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", shift.EmployeeId);
         ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name", shift.ScheduleId);
         ViewData["ShiftRoleId"] = new SelectList(_context.ShiftRole.Where(r => r.ScheduleId == shift.ScheduleId), "Id", "Name", shift.ShiftRoleId);
         return View(shift);
      }

      // GET: Shift/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var shift = await _context.Shift
             .Include(s => s.Employee)
             .Include(s => s.Schedule)
             .Include(s => s.ShiftRole)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (shift == null)
         {
            return NotFound();
         }

         return View(shift);
      }

      // POST: Shift/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var shift = await _context.Shift.FindAsync(id);
         _context.Shift.Remove(shift);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      [HttpGet]
      public JsonResult GetScheduleShiftRoles(int ScheduleId)
      {
         var shiftRoleList = new SelectList(_context.ShiftRole.Where(r => r.ScheduleId == ScheduleId), "Id", "Name");
         return Json(shiftRoleList);
      }

      private bool ShiftExists(int id)
      {
         return _context.Shift.Any(e => e.Id == id);
      }
   }
}
