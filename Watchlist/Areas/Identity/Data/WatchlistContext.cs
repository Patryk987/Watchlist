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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

}
