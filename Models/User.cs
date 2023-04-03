namespace Centhora_Hotels.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string? UserImageURL { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
