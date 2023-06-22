using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class ActorModel
    {
        [Key]
        public string id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string asCharacter { get; set; }

    }
}