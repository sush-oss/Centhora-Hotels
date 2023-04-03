namespace Centhora_Hotels.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomTypeId { get; set; }
        public DateOnly CheckIn_Date { get; set; }
        public DateOnly CheckOut_Date { get; set; }
        public int NumOfRoomsBooked { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual User User { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
