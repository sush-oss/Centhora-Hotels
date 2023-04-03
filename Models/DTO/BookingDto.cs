namespace Centhora_Hotels.Models.DTO
{
    public class BookingDto
    {
        public int UserId { get; set; }
        public int RoomTypeId { get; set; }
        public DateOnly CheckIn_Date { get; set; }
        public DateOnly CheckOut_Date { get; set; }
        public int NumOfRoomsBooked { get; set; }

    }
}
