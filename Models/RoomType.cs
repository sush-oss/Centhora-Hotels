namespace Centhora_Hotels.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public int MaxOccupance { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public virtual RoomPrice RoomPrice { get; set; }
    }
}
