using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Centhora_Hotels.DB_Context;
using Centhora_Hotels.InternalServices.Calculate_Room_Price;
using Centhora_Hotels.Models;
using Centhora_Hotels.Models.DTO;
using Centhora_Hotels.Repository.Interface;

namespace Centhora_Hotels.Repository.Repo
{
    public class BookingRepository : IBookingRepository, IDisposable
    {
        private readonly CenthoraDbContext _centhora;
        private readonly IMapper _mapper;
        private readonly ICalculateRoomPrices _calculateRoom;

        private bool disposedValue = false;

        public BookingRepository(CenthoraDbContext centhora, IMapper mapper, ICalculateRoomPrices calculateRoom)
        {
            this._centhora = centhora;
            this._mapper = mapper;
            this._calculateRoom = calculateRoom;

        }

        public async Task<bool> CreateBooking(BookingDto dto)
        {
            var roomType = await _centhora.Rooms.FindAsync(dto.RoomTypeId);
            var roomPrice =  await _centhora.Prices.FirstOrDefaultAsync(p => p.RoomTypeId == dto.RoomTypeId);

            if (roomType == null && roomPrice == null)
            {
                return false;
            }
            else
            {
                var booking = _mapper.Map<Booking>(dto);
                booking.RoomType = roomType;

                // Calculate the total price for the booking
                booking.TotalPrice = _calculateRoom.CalculateTotalPrice(dto.NumOfRoomsBooked, roomPrice.Price);

                _centhora.Bookings.Add(booking);
                await _centhora.SaveChangesAsync();

                return true;
            }            
        }

        public async Task DeleteBooking(int id)
        {
            // Check if the Booking exists
            var existingBooking = await _centhora.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);

            // If the booking exists, then remove it
            if (existingBooking != null)
            {
                _centhora.Bookings.Remove(existingBooking);
                await _centhora.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookings()
        {
            var allBookings = await _centhora.Bookings.ToListAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(allBookings);
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsByUserId(int id)
        {
            if(id < 0)
            {
                // If the id is null or less than 0, returning an empty collection.
                return new List<BookingDto>();
            }
            var userBookings = await _centhora.Bookings
                .Where(b => b.UserId == id)
                .ToListAsync();

            var bookingList = _mapper.Map<IEnumerable<BookingDto>>(userBookings);

            return bookingList;
        }

        public async Task<bool> UpdateBooking(int bookingId, UpdateBookingDto dto)
        {
            var existingBooking = await _centhora.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (existingBooking != null)
            {
                existingBooking.CheckIn_Date = dto.CheckIn_Date;
                existingBooking.CheckOut_Date = dto.CheckOut_Date;

                // Check if the RoomTypeId or NumOfRoomsBooked properties have been changed
                if (existingBooking.RoomTypeId != dto.RoomTypeId || existingBooking.NumOfRoomsBooked != dto.NumOfRoomsBooked)
                {
                    // Update RoomTypeId property
                    existingBooking.RoomTypeId = dto.RoomTypeId;

                    // Update Number of Rooms booked
                    existingBooking.NumOfRoomsBooked = dto.NumOfRoomsBooked;

                    // Get the RoomType entity for the new RoomTypeId
                    var newRoomType = await _centhora.Rooms.FirstOrDefaultAsync(rt => rt.RoomTypeId == dto.RoomTypeId);
                    if (newRoomType != null)
                    {
                        // Calculate the new TotalPrice using the ICalculateTotalRoomPrices instance
                        var totalPrice = _calculateRoom.CalculateTotalPrice(dto.NumOfRoomsBooked, newRoomType.RoomPrice.Price);

                        // Assign the new total price to TotalPrice property
                        existingBooking.TotalPrice = totalPrice;

                        // Update the RoomType property
                        existingBooking.RoomType = newRoomType;

                    }

                    // Save changes to the database
                    await _centhora.SaveChangesAsync();
                    return true;
                }

                // Save changes to the database
                await _centhora.SaveChangesAsync();
                return true;

            }
            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BookingRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
