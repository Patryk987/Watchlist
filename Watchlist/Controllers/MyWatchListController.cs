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
            var results = _DbContext.WatchList.Where(x => x.UserId == userData.Id).ToList();
            return View(results);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(string id, int seasons = 1)
        {
            this.GetOptions();

            var userData = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _DbContext.WatchList.Where(x => x.id == id)
                                                    .Where(c => c.UserId == userData.Id)
                                                    .Include("Episodes")
                                                    .FirstOrDefaultAsync();

            if (result != null)
            {

                List<string> watchedEpisodesList = await _DbContext.WatchedEpisodes
                                                                    .Where(c => c.UserId == userData.Id)
                                                                    .Select(w => w.IMDbEpisodesId)
                                                                    .ToListAsync();

                ViewBag.watchedEpisodesList = watchedEpisodesList;

                ViewBag.Episodes = await IMDbRepository.GetEpisodesList(result.IMDbId, seasons);
                ViewBag.SeriesDetails = await IMDbRepository.GetDetails(result.IMDbId);
                ViewBag.Seasons = seasons;

                return View(result);

            }
            else
            {
                return NotFound();
            }


        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(string id, WatchedEpisodesModel input, int seasons = 1)
        {
            await this.SaveEpisodes(input.IMDbEpisodesId, input.IMDbSeriesId);

            this.GetOptions();

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

            ViewBag.Episodes = await IMDbRepository.GetEpisodesList(result.IMDbId, seasons);
            ViewBag.SeriesDetails = await IMDbRepository.GetDetails(result.IMDbId);
            ViewBag.Seasons = seasons;

            return View(result);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string id, WatchListModel input, int seasons = 1)
        {

            this.GetOptions();

            var userData = await _userManager.GetUserAsync(HttpContext.User);

            await this.ChangeStatus(userData.Id, input.Status, id);

            var result = await _DbContext.WatchList.Where(x => x.id == id)
                                                    .Where(c => c.UserId == userData.Id)
                                                    .Include("Episodes")
                                                    .FirstOrDefaultAsync();

            List<string> watchedEpisodesList = await _DbContext.WatchedEpisodes
                                                                .Where(c => c.UserId == userData.Id)
                                                                .Select(w => w.IMDbEpisodesId)
                                                                .ToListAsync();

            ViewBag.watchedEpisodesList = watchedEpisodesList;

            ViewBag.Episodes = await IMDbRepository.GetEpisodesList(result.IMDbId, seasons);
            ViewBag.SeriesDetails = await IMDbRepository.GetDetails(result.IMDbId);
            ViewBag.Seasons = seasons;

            // return View(result);
            return RedirectToAction("Details", "MyWatchList", result);

        }

        // Delete

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            var userData = await _userManager.GetUserAsync(HttpContext.User);
            var results = _DbContext.WatchList.Where(w => w.id == id).FirstOrDefault();
            return View(results);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(string id, WatchListModel input)
        {

            var rent = _DbContext.WatchList.Where(w => w.id == id).FirstOrDefault();
            if (rent == null)
            {
                return NotFound();
            }

            _DbContext.WatchList.Remove(rent);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        private async Task<bool> SaveEpisodes(string IMDbEpisodesId, string IMDbSeriesId)
        {

            // TODO: Zwracanie danych

            var userData = await _userManager.GetUserAsync(HttpContext.User);

            var episodes = _DbContext.WatchedEpisodes.Where(w => w.UserId == userData.Id).Where(x => x.IMDbEpisodesId == IMDbEpisodesId).FirstOrDefault();
            if (episodes == null)
            {
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
            else
            {
                _DbContext.WatchedEpisodes.Remove(episodes);
                _DbContext.SaveChanges();
                return true;
            }


        }

        private async void GetOptions()
        {

            EWatchStatus[] enumValues = (EWatchStatus[])Enum.GetValues(typeof(EWatchStatus));

            List<SelectListModel> selectList = new List<SelectListModel>();

            foreach (EWatchStatus value in enumValues)
            {
                SelectListModel item = new SelectListModel
                {
                    Text = value.ToString(),
                    Value = value.ToString()
                };

                selectList.Add(item);
            }

            // Przekazanie listy opcji do widoku
            ViewBag.WatchStatusOptions = selectList;


        }

        private async Task<bool> ChangeStatus(string userId, EWatchStatus status, string seriesId)
        {


            var updates = await _DbContext.WatchList.Where(x => x.id == seriesId)
                                        .Where(c => c.UserId == userId)
                                        .FirstOrDefaultAsync();

            if (updates != null)
            {

                updates.Status = status;
                _DbContext.SaveChanges();

            }

            return true;

        }

    }
}