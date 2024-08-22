using System.ComponentModel.DataAnnotations;

namespace WatchList.Web.Models;

public class Movie
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    [DataType(DataType.Date), Display(Name = "Watched"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime WatchDate { get; set; } = DateTime.Now.Date;

    public int RatingId { get; set; }

    public Rating? Rating { get; set; }
}
