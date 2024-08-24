using Microsoft.EntityFrameworkCore;
using WatchedList.Web.Data;

namespace WatchedList.Web.Models;

public static class SeedData
{
    private readonly static IEnumerable<Rating> _ratings = [
        new Rating { Name = "Awful" },
        new Rating { Name = "Disappointing" },
        new Rating { Name = "Good" },
        new Rating { Name = "Great" },
        new Rating { Name = "Excellent" }
    ];


    public static void Initialise(IServiceProvider serviceProvider)
    {
        using var dataContext = new WatchedListDataContext(serviceProvider.GetRequiredService<DbContextOptions<WatchedListDataContext>>());

        // Required.
        SeedRating(dataContext);

        // Optional.
        SeedMovies(dataContext);
        SeedTheatricalPerformances(dataContext);
        SeedTvShows(dataContext);
    }

    private static void SeedRating(WatchedListDataContext dataContext)
    {

        foreach (Rating rating in _ratings)
        {
            if (dataContext.Rating.SingleOrDefault(x => x.Name == rating.Name) is null)
            {
                dataContext.Rating.Add(rating);
            }
        }

        dataContext.SaveChanges();
    }

    private static void SeedMovies(WatchedListDataContext dataContext)
    {
        if (dataContext.Movie.Any())
        {
            return;
        }

        dataContext.Movie.AddRange(
            new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Titanic",
                WatchDate = DateTime.Parse("1997-12-19"),
                RatingId = dataContext.Rating.Single(x => x.Name == "Good").Id
            },
             new Movie
             {
                 Id = Guid.NewGuid(),
                 Title = "E.T. the Extra-Terrestrial",
                 WatchDate = DateTime.Parse("1982-06-11"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Good").Id
             },
             new Movie
             {
                 Id = Guid.NewGuid(),
                 Title = "Star Wars: Episode IV - A New Hope",
                 WatchDate = DateTime.Parse("1977-05-25"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Great").Id
             },
            new Movie
            {
                Id = Guid.NewGuid(),
                Title = "The Lord of the Rings: The Return of the King",
                WatchDate = DateTime.Parse("2003-12-17"),
                RatingId = dataContext.Rating.Single(x => x.Name == "Excellent").Id
            },
             new Movie
             {
                 Id = Guid.NewGuid(),
                 Title = "Batman & Robin",
                 WatchDate = DateTime.Parse("1997-06-20"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Awful").Id
             },
             new Movie
             {
                 Id = Guid.NewGuid(),
                 Title = "Jingle All the Way",
                 WatchDate = DateTime.Parse("1996-11-22"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Disappointing").Id
             });

        dataContext.SaveChanges();
    }

    private static void SeedTvShows(WatchedListDataContext dataContext)
    {
        if (dataContext.TvShow.Any())
        {
            return;
        }

        dataContext.TvShow.AddRange(
            new TvShow
            {
                Id = Guid.NewGuid(),
                Title = "Game of Thrones",
                WatchDate = DateTime.Parse("2011-04-17"),
                RatingId = dataContext.Rating.Single(x => x.Name == "Excellent").Id
            },
             new TvShow
             {
                 Id = Guid.NewGuid(),
                 Title = "Stranger Things",
                 WatchDate = DateTime.Parse("2016-07-15"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Great").Id
             },
             new TvShow
             {
                 Id = Guid.NewGuid(),
                 Title = "Sons of Anarchy",
                 WatchDate = DateTime.Parse("2008-09-03"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Good").Id
             },
            new TvShow
            {
                Id = Guid.NewGuid(),
                Title = "The Walking Dead",
                WatchDate = DateTime.Parse("2010-10-31"),
                RatingId = dataContext.Rating.Single(x => x.Name == "Great").Id
            },
             new TvShow
             {
                 Id = Guid.NewGuid(),
                 Title = "Breaking Bad",
                 WatchDate = DateTime.Parse("2008-01-20"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Excellent").Id
             },
             new TvShow
             {
                 Id = Guid.NewGuid(),
                 Title = "Baywatch",
                 WatchDate = DateTime.Parse("1989-09-22"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Disappointing").Id
             },
             new TvShow
             {
                 Id = Guid.NewGuid(),
                 Title = "The Brady Bunch Variety Hour",
                 WatchDate = DateTime.Parse("1976-11-28"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Awful").Id
             });

        dataContext.SaveChanges();
    }

    private static void SeedTheatricalPerformances(WatchedListDataContext dataContext)
    {
        if (dataContext.TheatricalPerformance.Any())
        {
            return;
        }

        dataContext.TheatricalPerformance.AddRange(
             new TheatricalPerformance
             {
                 Id = Guid.NewGuid(),
                 Title = "Wicked",
                 WatchDate = DateTime.Parse("2017-08-15"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Good").Id
             },
             new TheatricalPerformance
             {
                 Id = Guid.NewGuid(),
                 Title = "The Phantom of the Opera",
                 WatchDate = DateTime.Parse("2018-09-03"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Disappointing").Id
             },
            new TheatricalPerformance
            {
                Id = Guid.NewGuid(),
                Title = "Les Misérables",
                WatchDate = DateTime.Parse("2019-01-25"),
                RatingId = dataContext.Rating.Single(x => x.Name == "Excellent").Id
            },
             new TheatricalPerformance
             {
                 Id = Guid.NewGuid(),
                 Title = "Frozen the Musical",
                 WatchDate = DateTime.Parse("2019-10-17"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Awful").Id
             },
             new TheatricalPerformance
             {
                 Id = Guid.NewGuid(),
                 Title = "Matilda the Musical",
                 WatchDate = DateTime.Parse("2024-04-21"),
                 RatingId = dataContext.Rating.Single(x => x.Name == "Great").Id
             }
             );

        dataContext.SaveChanges();
    }
}
