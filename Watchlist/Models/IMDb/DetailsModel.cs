using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class DetailsModel
    {
        [Key]
        public string id { get; set; }
        public string title { get; set; }
        public string? originalTitle { get; set; }
        public string? fullTitle { get; set; }
        public string? type { get; set; }
        public string? year { get; set; }
        public string? image { get; set; }
        public string? releaseDate { get; set; }
        public string? plot { get; set; }
        public List<ActorModel> actorList { get; set; }
        public string? imDbRating { get; set; }
        public string? imDbRatingVotes { get; set; }
        public string? metacriticRating { get; set; }
        public List<SimilarModel>? similars { get; set; }
        public TvSeriesInfoModel? tvSeriesInfo { get; set; }
    }
}