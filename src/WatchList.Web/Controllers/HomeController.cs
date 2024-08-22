using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WatchList.Web.Data;
using WatchList.Web.Models;

namespace WatchList.Web.Controllers;
public class HomeController : Controller
{
    private readonly WatchListDataContext _context;

    public HomeController(WatchListDataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["MoviesCount"] = _context.Movie.Count();
        ViewData["TvShowsCount"] = _context.TvShow.Count();
        ViewData["TheatricalPerformancesCount"] = _context.TheatricalPerformance.Count();

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
