using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchedList.Web.Data;
using WatchedList.Web.Models;

namespace WatchedList.Web.Controllers;

public class MoviesController : Controller
{
    private readonly WatchedListDataContext _context;

    public MoviesController(WatchedListDataContext context)
    {
        _context = context;
    }

    // GET: Movies
    public async Task<IActionResult> Index(int rating, string searchString)
    {
        if (_context.Movie is null)
        {
            return Problem($"Entity set '{nameof(_context.Movie)}' is null.");
        }

        var ratings = _context.Rating.AsQueryable();
        var movies = _context.Movie.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
        }

        if (rating > 0)
        {
            movies = movies.Where(r => r.Rating!.Id == rating);
        }

        var model = new MovieView
        {
            Movies = await movies.ToListAsync(),
            Ratings = new SelectList(await ratings.OrderBy(o => o.Id).ToListAsync(), "Id", "Name")
        };

        return View(model);
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name");
        return View();
    }

    // POST: Movies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,WatchDate,RatingId")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            movie.Id = Guid.NewGuid();
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name", movie.RatingId);
        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name", movie.RatingId);
        return View(movie);
    }

    // POST: Movies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,WatchDate,RatingId")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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
        ViewData["RatingId"] = new SelectList(_context.Set<Rating>(), "Id", "Name", movie.RatingId);
        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Rating)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(Guid id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
