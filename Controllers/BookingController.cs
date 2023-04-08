using Centhora_Hotels.Models.DTO;
using Centhora_Hotels.Repository.Interface;
using Centhora_Hotels.Repository.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Centhora_Hotels.Controllers
{
    
    //[Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ILogger<BookingController> logger;
        public BookingController(IBookingRepository _bookingRepository, ILogger<BookingController> _logger)
        {
            bookingRepository = _bookingRepository;
            logger = _logger;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/allbookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBooking()
        {
            var allBookings = await bookingRepository.GetAllBookings();
            return Ok(allBookings);
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/newbooking")]
        public async Task<ActionResult<BookingDto>> CreateNewBooking(BookingDto booking)
        {
            try
            {
                var newBooking = await bookingRepository.CreateBooking(booking);
                return Ok(newBooking);
            }
            catch (Exception ex)
            {
                logger.LogError("Internal Server Error! Unable to create the booking for user ID: ", booking.UserId);
                return StatusCode(500, ex.Message);
            }
            
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/getbookinsbyuserid/{id}")]
        public async Task<ActionResult> GetBookingsByUser(int id)
        {
            var bookings = await bookingRepository.GetAllBookingsByUserId(id);
            return Ok(bookings);
        }
    }
}
