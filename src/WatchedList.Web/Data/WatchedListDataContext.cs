using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WatchedList.Web.Models;

namespace WatchedList.Web.Data
{
    public class WatchedListDataContext : DbContext
    {
        public WatchedListDataContext(DbContextOptions<WatchedListDataContext> options)
            : base(options)
        {
        }

        public DbSet<Rating> Rating { get; set; } = default!;
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<TvShow> TvShow { get; set; } = default!;
        public DbSet<TheatricalPerformance> TheatricalPerformance { get; set; } = default!;
    }
}
