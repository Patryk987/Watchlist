using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class IMDbModel
    {
        [Key]
        public string searchType { get; set; }
        public string expression { get; set; }
        public List<ResultsModel> results { get; set; }

    }
}