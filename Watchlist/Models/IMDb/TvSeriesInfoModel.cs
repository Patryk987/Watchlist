using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Models
{
    public class TvSeriesInfoModel
    {
        [Key]
        public string id { get; set; }
        public string creators { get; set; }
        public List<int> seasons { get; set; }

    }
}