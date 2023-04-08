using Centhora_Hotels.Models.DTO;
using Centhora_Hotels.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Centhora_Hotels.Controllers
{
    public static class BookingEndpoints
    {
        
        public static void MapBookingMinimalApiEndpoints(this WebApplication app)
        {
            app.MapGet("api/v2/allbookings", GetAllBooking).RequireAuthorization();
            app.MapPost("api/v2/newbooking", CreateNewBooking).RequireAuthorization();
            app.MapGet("api/v2/userbookings/{id}", GetBookingsByUser).RequireAuthorization();
        }

        public static async Task<IResult> GetAllBooking(IBookingRepository bookingRepository)
        {
            var allBookings = await bookingRepository.GetAllBookings();
            return Results.Ok(allBookings);
        }

        
        public static async Task<IResult> CreateNewBooking(BookingDto booking, IBookingRepository bookingRepository)
        {
            
             var newBooking = await bookingRepository.CreateBooking(booking);
             return Results.Ok(newBooking);
            
        }

        
        public static async Task<IResult> GetBookingsByUser(int id, IBookingRepository bookingRepository)
        {
            var bookings = await bookingRepository.GetAllBookingsByUserId(id);
            return Results.Ok(bookings);
        }
    }
}
