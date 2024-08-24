using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WatchedList.Web.Models;

public class TvShowView
{
    public List<TvShow>? TvShows { get; set; }

    public SelectList? Ratings { get; set; }

    public Rating? Rating { get; set; }

    [Display(Name = "Search")]
    public string? SearchString { get; set; }
}
