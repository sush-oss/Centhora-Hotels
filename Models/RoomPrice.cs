using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centhora_Hotels.Models
{
    public class RoomPrice
    {
        [Key]
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; } //The primary key of the Parent table (RoomType) is the primary and foreign key for dependant
                                            // table (RoomPrice).
        public decimal Price { get; set; }
        public RoomType RoomType { get; set; }
    }
}
