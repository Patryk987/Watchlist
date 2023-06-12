using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class WatchListModel
    {
        [Key]
        public string id { get; set; }
        public string UserId { get; set; }
        public string IMDbId { get; set; }
        public DateTime StartWatch { get; set; }
        public List<WatchedEpisodesModel>? Episodes { get; set; }

    }
}