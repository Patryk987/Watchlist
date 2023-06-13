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

            return View(result);

        }

    }
}