using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly string _apiKey = "k_r3d4g61z";

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync(string q)
        {
            string apiUrl = $"https://imdb-api.com/en/API/SearchSeries/{_apiKey}/{q}";
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            string apiUrl = $"https://imdb-api.com/en/API/Title/{_apiKey}/{id}";

            Console.WriteLine(apiUrl);

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}