using Microsoft.AspNetCore.Mvc.Rendering;

namespace WatchList.Web.Models;

public class TheatricalPerformanceView
{
    public List<TheatricalPerformance>? TheatricalPerformances { get; set; }

    public SelectList? Ratings { get; set; }

    public Rating? Rating { get; set; }

    public string? SearchString { get; set; }
}
