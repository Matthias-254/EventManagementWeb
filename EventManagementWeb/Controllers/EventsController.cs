﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagementWeb.Data;
using EventManagementWeb.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using EventManagementWeb.Services;

namespace EventManagementWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EventManagementUser _user;

        public EventsController(ApplicationDbContext context, IMyUser user)
        {
            _context = context;
            _user = user.User;
        }

        // GET: Events
        public async Task<IActionResult> Index(string name = "", string description = "", int location = 1)
        {
            try
            {
                var applicationDbContext = _context.Events.Include(e => e.Location)
                                            .Where(e => e.Deleted > DateTime.Now
                                                    && (string.IsNullOrEmpty(name) || e.Name.Contains(name))
                                                    && (string.IsNullOrEmpty(description) || e.Description.Contains(description))
                                                    && (location == 1 || e.LocationId == location))
                                            .OrderBy(e => e.Name)
                                            .Include(e => e.StartedBy);

                ViewBag.Name = name;
                ViewBag.Description = description;
                ViewBag.Locations = new SelectList(_context.Locations, "Id", "Name", location);

                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout opgetreden: {ex.Message}");

                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de evenementen.";
                return View(Enumerable.Empty<Event>().ToList());
            }
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.StartedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address");
            ViewData["StartedById"] = new SelectList(_context.EventManagementUsers, "Id", "Id");
            return View(new Event());
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,LocationId,StartedById,Deleted")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", @event.LocationId);
            ViewData["StartedById"] = new SelectList(_context.EventManagementUsers, "Id", "Id", @event.StartedById);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", @event.LocationId);
            ViewData["StartedById"] = new SelectList(_context.EventManagementUsers, "Id", "Id", @event.StartedById);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,LocationId,StartedById,Deleted")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", @event.LocationId);
            ViewData["StartedById"] = new SelectList(_context.EventManagementUsers, "Id", "Id", @event.StartedById);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.StartedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                @event.Deleted = DateTime.Now;
                _context.Events.Update(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
