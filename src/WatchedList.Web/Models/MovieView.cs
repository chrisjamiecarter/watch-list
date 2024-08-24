using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WatchedList.Web.Models;

public class MovieView
{
    public List<Movie>? Movies { get; set; }

    public SelectList? Ratings { get; set; }

    public Rating? Rating { get; set; }
    
    public int? RatingId { get; set; }

    [Display(Name = "Search")]
    public string? SearchString { get; set; }

    public string? CurrentSort { get; set; }

    public string? TitleSort { get; set; }

    public string? WatchDateSort { get; set; }

    public string? RatingSort { get; set; }
}
