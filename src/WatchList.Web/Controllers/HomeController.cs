using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
