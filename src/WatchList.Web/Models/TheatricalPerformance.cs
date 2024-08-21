using System.ComponentModel.DataAnnotations;

namespace WatchList.Web.Models;

public class TheatricalPerformance
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime WatchDate { get; set; } = DateTime.Now.Date;

    public int RatingId { get; set; }

    public Rating? Rating { get; set; }
}
