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
        private readonly string _apiKey = "k_r3d4g61z";

        private readonly WatchlistContext _DbContext;
        private readonly UserManager<WatchlistUser> _userManager;

        public SearchController(ILogger<SearchController> logger, UserManager<WatchlistUser> userManager, WatchlistContext context)
        {
            _userManager = userManager;
            _DbContext = context;
            _logger = logger;
        }


        [Authorize]
        public async Task<IActionResult> IndexAsync(string q)
        {
            if (q != null)
            {

                string apiUrl = $"https://imdb-api.com/en/API/Search/{_apiKey}/{q}";
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();

                        var data = JsonConvert.DeserializeObject<IMDbModel>(responseBody);

                        ViewBag.expression = data.expression;

                        List<ResultsModel> results = new List<ResultsModel>();

                        foreach (var item in data.results)
                        {
                            results.Add(new ResultsModel()
                            {
                                id = item.id,
                                resultType = item.resultType,
                                image = item.image,
                                title = item.title,
                                description = item.description,
                            });

                        }


                        return View(results);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Wystąpił błąd podczas pobierania danych: {e.Message}");
                    }
                }
                return NotFound();
            }
            else
            {
                List<ResultsModel> results = new List<ResultsModel>();
                return View(results);
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            string apiUrl = $"https://imdb-api.com/en/API/Title/{_apiKey}/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<DetailsModel>(responseBody);


                    return View(data);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Wystąpił błąd podczas pobierania danych: {e.Message}");
                }
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
                StartWatch = new DateTime()
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