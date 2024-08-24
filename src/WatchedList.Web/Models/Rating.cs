namespace WatchedList.Web.Models;

public class Rating
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Movie> Movies { get; set; }
    public List<TvShow> TvShows { get; set; }
    public List<TheatricalPerformance> TheatricalPerformances { get; set; }
}
