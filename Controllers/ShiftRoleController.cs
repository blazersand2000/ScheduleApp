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
    public class ShiftRoleController : Controller
    {
        private readonly ScheduleAppContext _context;

        public ShiftRoleController(ScheduleAppContext context)
        {
            _context = context;
        }

        // GET: ShiftRole
        public async Task<IActionResult> Index()
        {
            var scheduleAppContext = _context.ShiftRole.Include(s => s.Schedule);
            return View(await scheduleAppContext.ToListAsync());
        }

        // GET: ShiftRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftRole = await _context.ShiftRole
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftRole == null)
            {
                return NotFound();
            }

            return View(shiftRole);
        }

        // GET: ShiftRole/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name");
            return View();
        }

        // POST: ShiftRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ScheduleId")] ShiftRole shiftRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name", shiftRole.ScheduleId);
            return View(shiftRole);
        }

        // GET: ShiftRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftRole = await _context.ShiftRole.FindAsync(id);
            if (shiftRole == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name", shiftRole.ScheduleId);
            return View(shiftRole);
        }

        // POST: ShiftRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ScheduleId")] ShiftRole shiftRole)
        {
            if (id != shiftRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftRoleExists(shiftRole.Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Name", shiftRole.ScheduleId);
            return View(shiftRole);
        }

        // GET: ShiftRole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftRole = await _context.ShiftRole
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftRole == null)
            {
                return NotFound();
            }

            return View(shiftRole);
        }

        // POST: ShiftRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftRole = await _context.ShiftRole.FindAsync(id);
            _context.ShiftRole.Remove(shiftRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftRoleExists(int id)
        {
            return _context.ShiftRole.Any(e => e.Id == id);
        }
    }
}
