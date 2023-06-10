namespace Watchlist.Models
{
    public class SeriesModel
    {
        public int Id { get; set; }
        public string IMDbId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<SeasonsModel>? Seasons { get; set; }
    }
}
