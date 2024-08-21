using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WatchList.Web.Models;

namespace WatchList.Web.Data
{
    public class WatchListDataContext : DbContext
    {
        public WatchListDataContext (DbContextOptions<WatchListDataContext> options)
            : base(options)
        {
        }

        public DbSet<Rating> Rating { get; set; } = default!;
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<TvShow> TvShow { get; set; } = default!;
        public DbSet<TheatricalPerformance> TheatricalPerformance { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().HasData(
                new Rating { Id = 1, Name = "Awful"},
                new Rating { Id = 2, Name = "Disappointing" },
                new Rating { Id = 3, Name = "Good" },
                new Rating { Id = 4, Name = "Great" },
                new Rating { Id = 5, Name = "Excellent" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
