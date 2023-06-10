namespace Watchlist.Models
{
    public class EpisodeModel
    {
        public int Id { get; set; }
        public string IMDbId { get; set; }
        public string Title { get; set; }
        public string EpisodeNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Plot { get; set; }
        public string? imDbRating { get; set; }
        public SeasonsModel Seasons { get; set; }

    }
}
