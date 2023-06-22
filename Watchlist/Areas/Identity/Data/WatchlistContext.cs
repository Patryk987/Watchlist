using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watchlist.Areas.Identity.Data;
using Watchlist.Models;

namespace Watchlist.Data;

public class WatchlistContext : IdentityDbContext<WatchlistUser>
{
    public WatchlistContext(DbContextOptions<WatchlistContext> options)
        : base(options)
    {
    }

    public DbSet<WatchListModel> WatchList { get; set; }
    public DbSet<WatchedEpisodesModel> WatchedEpisodes { get; set; }

    // IMDb
    // public DbSet<ActorModel> ActorModel { get; set; }
    // public DbSet<DetailsModel> DetailsModel { get; set; }
    // public DbSet<EpisodesListModel> EpisodesListModel { get; set; }
    // public DbSet<EpisodesModel> EpisodesModel { get; set; }
    // public DbSet<IMDbModel> IMDbModel { get; set; }
    // public DbSet<ResultsModel> ResultsModel { get; set; }
    // public DbSet<SimilarModel> SimilarModel { get; set; }
    // public DbSet<TvSeriesInfoModel> TvSeriesInfoModel { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<List<int>>().HasNoKey();
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

}
