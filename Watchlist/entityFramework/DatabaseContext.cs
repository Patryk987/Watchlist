using Microsoft.EntityFrameworkCore;
using Watchlist.Models;

namespace Watchlist.entityFramework
{
    public class DatabaseContext
    {
        private string _connectionString = "Data Source=DESKTOP-RUE96KJ;Initial Catalog=Watchlist;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DbSet<SeriesModel> SeriesModel { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //    //modelBuilder.Entity<CassettesClientsHelpModel>(builder =>
        //    //{
        //    //    builder.HasNoKey();
        //    //    builder.ToTable("CassettesClientsHelpModel");
        //    //});

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}

    }
}
