
namespace Watchlist.Models
{
    public class WatchedEpisodesModel
    {
        public string id { get; set; }
        public string IMDbEpisodesId { get; set; }
        public string IMDbSeriesId { get; set; }
        public string UserId { get; set; }
        public DateTime WatchDate { get; set; }
        public bool IsWatched { get; set; }

    }
}