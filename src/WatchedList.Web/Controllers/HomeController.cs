using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchedList.Web.Data;
using WatchedList.Web.Models;

namespace WatchedList.Web.Controllers;
public class HomeController : Controller
{
    private readonly WatchedListDataContext _context;

    public HomeController(WatchedListDataContext context)
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
