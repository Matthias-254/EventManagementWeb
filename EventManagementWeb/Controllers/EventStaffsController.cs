using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagementWeb.Data;
using EventManagementWeb.Models;

namespace EventManagementWeb.Controllers
{
    public class EventStaffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventStaffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventStaffs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventStaff.Where(e => e.Deleted > DateTime.Now).Include(e => e.Event).Include(e => e.Staff);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventStaff = await _context.EventStaff
                .Include(e => e.Event)
                .Include(e => e.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventStaff == null)
            {
                return NotFound();
            }

            return View(eventStaff);
        }

        // GET: EventStaffs/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description");
            ViewData["StaffId"] = new SelectList(_context.EventManagementUsers, "Id", "Id");
            return View();
        }

        // POST: EventStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,StaffId,BecameMemberDate,DoneById,Remark,Deleted")] EventStaff eventStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventStaff.EventId);
            ViewData["StaffId"] = new SelectList(_context.EventManagementUsers, "Id", "Id", eventStaff.StaffId);
            return View(eventStaff);
        }

        // GET: EventStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventStaff = await _context.EventStaff.FindAsync(id);
            if (eventStaff == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventStaff.EventId);
            ViewData["StaffId"] = new SelectList(_context.EventManagementUsers, "Id", "Id", eventStaff.StaffId);
            return View(eventStaff);
        }

        // POST: EventStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,StaffId,BecameMemberDate,DoneById,Remark,Deleted")] EventStaff eventStaff)
        {
            if (id != eventStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventStaffExists(eventStaff.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventStaff.EventId);
            ViewData["StaffId"] = new SelectList(_context.EventManagementUsers, "Id", "Id", eventStaff.StaffId);
            return View(eventStaff);
        }

        // GET: EventStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventStaff = await _context.EventStaff
                .Include(e => e.Event)
                .Include(e => e.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventStaff == null)
            {
                return NotFound();
            }

            return View(eventStaff);
        }

        // POST: EventStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventStaff = await _context.EventStaff.FindAsync(id);
            if (eventStaff != null)
            {
                eventStaff.Deleted = DateTime.Now;
                _context.EventStaff.Update(eventStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventStaffExists(int id)
        {
            return _context.EventStaff.Any(e => e.Id == id);
        }
    }
}
