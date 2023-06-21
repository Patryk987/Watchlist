using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Watchlist.Models;

using Microsoft.AspNetCore.Identity;
using Watchlist.Areas.Identity.Data;
using Watchlist.Data;

namespace Watchlist.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly WatchlistContext _DbContext;
        private readonly UserManager<WatchlistUser> _userManager;

        public SearchController(ILogger<SearchController> logger, UserManager<WatchlistUser> userManager, WatchlistContext context)
        {
            _userManager = userManager;
            _DbContext = context;
            _logger = logger;
        }


        [Authorize]
        public async Task<IActionResult> IndexAsync(string parameter)
        {
            if (parameter != null)
            {

                ViewBag.SomeProperty = parameter;

                var searchData = await IMDbRepository.Search(parameter);

                if (searchData != null)
                {

                    return View(searchData);

                }

            }
            else
            {
                List<ResultsModel> results = new List<ResultsModel>();
                return View(results);
            }

            return NotFound();

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DetailsAsync(string id)
        {

            var details = await IMDbRepository.GetDetails(id);
            ViewBag.Episodes = await IMDbRepository.GetEpisodesList(id, 1);

            if (details != null)
            {
                return View(details);
            }

            return NotFound();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(WatchListModel inputData)
        {
            var userData = await _userManager.GetUserAsync(HttpContext.User);

            var dataToDatabase = new WatchListModel
            {
                id = Guid.NewGuid().ToString(),
                UserId = userData.Id,
                IMDbId = inputData.id,
                Title = inputData.Title,
                Status = inputData.Status,
                ImageUrl = inputData.ImageUrl,
                StartWatch = DateTime.Now
            };

            _DbContext.WatchList.Add(dataToDatabase);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}