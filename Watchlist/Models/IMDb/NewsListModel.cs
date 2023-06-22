using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class NewsListModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string year { get; set; }
        public string releaseState { get; set; }
        public string image { get; set; }
        public string plot { get; set; }
        public string genres { get; set; }

    }
}