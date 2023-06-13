using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Watchlist.Models;


namespace Watchlist
{
    public class IMDbRepository
    {

        // TODO: PRzenieść api key na zewnątrz
        private static readonly string _apiKey = "k_r3d4g61z";

        public static async Task<List<ResultsModel>> Search(string title)
        {

            string apiUrl = $"https://imdb-api.com/en/API/Search/{_apiKey}/{title}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    List<ResultsModel> results = new List<ResultsModel>();

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<IMDbModel>(responseBody);

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

                    return results;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Wystąpił błąd podczas pobierania danych: {e.Message}");
                }
            }

            return null;
        }


        public static async Task<DetailsModel> GetDetails(string filmId)
        {
            string apiUrl = $"https://imdb-api.com/en/API/Title/{_apiKey}/{filmId}";



            using (HttpClient client = new HttpClient())
            {
                try
                {
                    DetailsModel results = new DetailsModel();
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    results = JsonConvert.DeserializeObject<DetailsModel>(responseBody);

                    return results;

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Wystąpił błąd podczas pobierania danych: {e.Message}");
                }
            }

            return null;


        }


    }
}