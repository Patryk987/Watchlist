using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Watchlist.Areas.Identity.Data;
using Watchlist.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WatchlistContextConnection") ?? throw new InvalidOperationException("Connection string 'WatchlistContextConnection' not found.");

builder.Services.AddDbContext<WatchlistContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WatchlistUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WatchlistContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
