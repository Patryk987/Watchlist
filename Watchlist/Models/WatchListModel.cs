using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{

    public enum EWatchStatus
    {
        NotStarted,
        Watching,
        Completed,
        OnHold,
        Dropped
    }

    public enum EType
    {
        Movie,
        Series,
        Other
    }

    public class WatchListModel
    {
        [Key]
        public string id { get; set; }
        public string IMDbId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public EWatchStatus Status { get; set; }
        public EType Type { get; set; } = EType.Other;
        public DateTime StartWatch { get; set; }
        public List<WatchedEpisodesModel>? Episodes { get; set; }
    }
}