using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using Watchlist.Areas.Identity.Data;
using Watchlist.Data;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    public class MyWatchListController : Controller
    {
        private readonly WatchlistContext _DbContext;
        private readonly UserManager<WatchlistUser> _userManager;

        public MyWatchListController(UserManager<WatchlistUser> userManager, WatchlistContext context)
        {
            _userManager = userManager;
            _DbContext = context;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userData = await _userManager.GetUserAsync(HttpContext.User);
            Console.WriteLine(userData.Id);
            var results = _DbContext.WatchList.Where(x => x.UserId == userData.Id).ToList();
            return View(results);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var userData = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _DbContext.WatchList.Where(x => x.id == id)
                                                    .Where(c => c.UserId == userData.Id)
                                                    .Include("Episodes")
                                                    .FirstOrDefaultAsync();

            List<string> watchedEpisodesList = await _DbContext.WatchedEpisodes
                                                                .Where(c => c.UserId == userData.Id)
                                                                .Select(w => w.IMDbEpisodesId)
                                                                .ToListAsync();

            ViewBag.watchedEpisodesList = watchedEpisodesList;

            ViewBag.Episodes = await IMDbRepository.GetEpisodesList(result.IMDbId, 1);
            ViewBag.SeriesDetails = await IMDbRepository.GetDetails(result.IMDbId);

            return View(result);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(string id, WatchedEpisodesModel input)
        {
            await this.SaveEpisodes(input.IMDbEpisodesId, input.IMDbSeriesId);

            var userData = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _DbContext.WatchList.Where(x => x.id == id)
                                                    .Where(c => c.UserId == userData.Id)
                                                    .Include("Episodes")
                                                    .FirstOrDefaultAsync();

            List<string> watchedEpisodesList = await _DbContext.WatchedEpisodes
                                                                .Where(c => c.UserId == userData.Id)
                                                                .Select(w => w.IMDbEpisodesId)
                                                                .ToListAsync();

            ViewBag.watchedEpisodesList = watchedEpisodesList;

            ViewBag.Episodes = await IMDbRepository.GetEpisodesList(result.IMDbId, 1);
            ViewBag.SeriesDetails = await IMDbRepository.GetDetails(result.IMDbId);

            return View(result);

        }

        private async Task<bool> SaveEpisodes(string IMDbEpisodesId, string IMDbSeriesId)
        {

            // TODO: Zmienić na toggle / Zwracanie danych

            var userData = await _userManager.GetUserAsync(HttpContext.User);

            var dataToDatabase = new WatchedEpisodesModel
            {
                id = Guid.NewGuid().ToString(),
                IMDbEpisodesId = IMDbEpisodesId,
                IMDbSeriesId = IMDbSeriesId,
                WatchDate = DateTime.Now,
                UserId = userData.Id,
                IsWatched = true
            };

            _DbContext.WatchedEpisodes.Add(dataToDatabase);
            _DbContext.SaveChanges();

            return true;

        }

    }
}