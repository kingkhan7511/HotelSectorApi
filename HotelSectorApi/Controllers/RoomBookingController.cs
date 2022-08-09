using HostelSector.Models.Base;
using HotelSector.ApplicationServices.RoomBooking;
using HotelSector.Models.RoomBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSectorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingService _roomBookingService;
        public RoomBookingController(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }
        [HttpPost("AddUpdateBooking")]
        public ApiResponseDto AddUpdateBooking(RoomBookingInputDto inputDto)
        {
            return _roomBookingService.AddUpdateBooking(inputDto);
        }
        

        [HttpPost("GetMyBooked")]
        public ApiResponseDto GetMyBooked(GetMyBookedRoomsInputDto inputDto)
        {
            return _roomBookingService.GetMyBookedRooms(inputDto);
        }

        [HttpDelete("DeleteBooking")]
        public ApiResponseDto DeleteBooking(RoomBookingDeleteInputDto inputDto)
        {
            return _roomBookingService.DeleteBooking(inputDto);
        }
    }
}
