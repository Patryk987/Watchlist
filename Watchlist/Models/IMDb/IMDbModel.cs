
namespace Watchlist.Models
{
    public class IMDbModel
    {
        public string searchType { get; set; }
        public string expression { get; set; }
        public List<ResultsModel> results { get; set; }

    }
}