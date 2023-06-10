namespace Watchlist.Models
{
    public class SeasonsModel
    {
        public int Id { get; set; }
        public string SesonNumber { get; set; }
        public DateTime ReleasedData { get; set; }
        public SeriesModel Series { get; set; }
        public List<EpisodeModel> Episodes { get; set; }
    }
}
