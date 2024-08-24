using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchedList.Web.Data;
using WatchedList.Web.Models;

namespace WatchedList.Web.Controllers;

public class TheatricalPerformancesController : Controller
{
    private readonly WatchedListDataContext _context;

    public TheatricalPerformancesController(WatchedListDataContext context)
    {
        _context = context;
    }

    // GET: TheatricalPerformances
    public async Task<IActionResult> Index(int rating, string searchString, string sortOrder)
    {
        if (_context.TheatricalPerformance is null)
        {
            return Problem($"Entity set '{nameof(_context.TheatricalPerformance)}' is null.");
        }

        var ratings = _context.Rating.AsQueryable();
        var theatricalPerformances = _context.TheatricalPerformance.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            theatricalPerformances = theatricalPerformances.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
        }

        if (rating > 0)
        {
            theatricalPerformances = theatricalPerformances.Where(r => r.Rating!.Id == rating);
        }

        var model = new TheatricalPerformanceView
        {
            TheatricalPerformances = await theatricalPerformances.ToListAsync(),
            Ratings = new SelectList(await ratings.OrderBy(o => o.Id).ToListAsync(), "Id", "Name")
        };

        model.RatingId = rating;
        model.SearchString = searchString;

        // Sort:
        model.CurrentSort = string.IsNullOrEmpty(sortOrder) ? "title_asc" : sortOrder;
        model.TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        model.WatchDateSort = sortOrder == "watchdate_asc" ? "watchdate_desc" : "watchdate_asc";
        model.RatingSort = sortOrder == "rating_asc" ? "rating_desc" : "rating_asc";

        model.TheatricalPerformances = sortOrder switch
        {
            "title_desc" => model.TheatricalPerformances.OrderByDescending(o => o.Title).ToList(),
            "watchdate_asc" => model.TheatricalPerformances.OrderBy(o => o.WatchDate).ToList(),
            "watchdate_desc" => model.TheatricalPerformances.OrderByDescending(o => o.WatchDate).ToList(),
            "rating_asc" => model.TheatricalPerformances.OrderBy(o => o.RatingId).ToList(),
            "rating_desc" => model.TheatricalPerformances.OrderByDescending(o => o.RatingId).ToList(),
            _ => model.TheatricalPerformances.OrderBy(o => o.Title).ToList(),
        };

        return View(model);
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
