using Microsoft.AspNetCore.Mvc.Rendering;

namespace WatchList.Web.Models;

public class TvShowView
{
    public List<TvShow>? TvShows { get; set; }

    public SelectList? Ratings { get; set; }

    public Rating? Rating { get; set; }

    public string? SearchString { get; set; }
}
