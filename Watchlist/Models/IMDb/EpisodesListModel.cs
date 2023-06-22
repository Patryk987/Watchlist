using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class EpisodesListModel
    {
        [Key]
        public string imDbId { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string type { get; set; }
        public string year { get; set; }
        public List<EpisodesModel> episodes { get; set; }

    }
}