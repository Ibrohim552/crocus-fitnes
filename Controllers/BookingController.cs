using AutoMapper;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using GymSphere.Services.BookingService;
using Microsoft.AspNetCore.Mvc;

namespace GymSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] BookingFilter filter)
        {
            var result = await _bookingService.GetBookings(filter);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var result = await _bookingService.GetBookingById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateInfo bookingCreateInfo)
        {
            var result = await _bookingService.CreateBooking(bookingCreateInfo);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateInfo bookingUpdateInfo)
        {
            var result = await _bookingService.UpdateBooking(id, bookingUpdateInfo);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _bookingService.DeleteBooking(id);
            return result.ToActionResult();
        }
    }
}
