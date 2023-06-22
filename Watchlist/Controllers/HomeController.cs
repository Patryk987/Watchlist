using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Watchlist.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Watchlist.Areas.Identity.Data;
using Watchlist.Data;

namespace Watchlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly WatchlistContext _DbContext;
        private readonly UserManager<WatchlistUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<WatchlistUser> userManager, WatchlistContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _DbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.news = await IMDbRepository.GetNews();
            ViewBag.LastWatch = await this.LastWatch();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<WatchListModel>> LastWatch()
        {

            var userData = await _userManager.GetUserAsync(HttpContext.User);
            if (userData != null)
            {

                List<string> watchedEpisodesList = await _DbContext.WatchedEpisodes
                                                                     .Where(c => c.UserId == userData.Id)
                                                                     .OrderBy(x => x.WatchDate)
                                                                     .Select(w => w.IMDbSeriesId)
                                                                     .Distinct()
                                                                     .Take(4)
                                                                     .ToListAsync();

                List<WatchListModel> result = await _DbContext.WatchList
                                                        .Where(x => watchedEpisodesList.Contains(x.IMDbId))
                                                        .Where(c => c.UserId == userData.Id)
                                                        .ToListAsync();


                return result;
            }

            return null;

        }
    }
}