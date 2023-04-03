using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.Repository.Interface
{
    public interface IBookingRepository : IDisposable
    {
        Task<IEnumerable<BookingDto>> GetAllBookings();
        Task<IEnumerable<BookingDto>> GetAllBookingsByUserId(int id);
        Task<bool> CreateBooking(BookingDto dto);
        Task<bool> UpdateBooking(int bookingId, UpdateBookingDto dto);
        Task DeleteBooking(int id);
    }
}
