using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WatchList.Web.Models;

public class MovieView
{
    public List<Movie>? Movies { get; set; }

    public SelectList? Ratings { get; set; }

    public Rating? Rating { get; set; }

    [Display(Name = "Search")]
    public string? SearchString { get; set; }
}
