using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class SimilarModel
    {
        [Key]
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imDbRating { get; set; }
    }
}