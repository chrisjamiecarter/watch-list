﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchList.Web.Data;
using WatchList.Web.Models;

namespace WatchList.Web.Controllers
{
    public class TheatricalPerformancesController : Controller
    {
        private readonly WatchListDataContext _context;

        public TheatricalPerformancesController(WatchListDataContext context)
        {
            _context = context;
        }

        // GET: TheatricalPerformances
        public async Task<IActionResult> Index()
        {
            var watchListDataContext = _context.TheatricalPerformance.Include(t => t.Rating);
            return View(await watchListDataContext.ToListAsync());
        }

        // GET: TheatricalPerformances/Create
        public IActionResult Create()
        {
            ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name");
            return View();
        }

        // POST: TheatricalPerformances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,WatchDate,RatingId")] TheatricalPerformance theatricalPerformance)
        {
            if (ModelState.IsValid)
            {
                theatricalPerformance.Id = Guid.NewGuid();
                _context.Add(theatricalPerformance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name", theatricalPerformance.RatingId);
            return View(theatricalPerformance);
        }

        // GET: TheatricalPerformances/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatricalPerformance = await _context.TheatricalPerformance.FindAsync(id);
            if (theatricalPerformance == null)
            {
                return NotFound();
            }
            ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name", theatricalPerformance.RatingId);
            return View(theatricalPerformance);
        }

        // POST: TheatricalPerformances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,WatchDate,RatingId")] TheatricalPerformance theatricalPerformance)
        {
            if (id != theatricalPerformance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theatricalPerformance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheatricalPerformanceExists(theatricalPerformance.Id))
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
            ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name", theatricalPerformance.RatingId);
            return View(theatricalPerformance);
        }

        // GET: TheatricalPerformances/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatricalPerformance = await _context.TheatricalPerformance
                .Include(t => t.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theatricalPerformance == null)
            {
                return NotFound();
            }

            return View(theatricalPerformance);
        }

        // POST: TheatricalPerformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var theatricalPerformance = await _context.TheatricalPerformance.FindAsync(id);
            if (theatricalPerformance != null)
            {
                _context.TheatricalPerformance.Remove(theatricalPerformance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheatricalPerformanceExists(Guid id)
        {
            return _context.TheatricalPerformance.Any(e => e.Id == id);
        }
    }
}
