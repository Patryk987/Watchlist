
namespace Watchlist.Models
{
    public class WatchedEpisodesModel
    {
        public string id { get; set; }
        public string IMDbEmpisodesId { get; set; }
        public string IMDbSeriesId { get; set; }
        public DateTime WatchDate { get; set; }
        public bool IsWatched { get; set; }

    }
}