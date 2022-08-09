using HostelSector.Models.Base;
using HotelSector.ApplicationServices.Room;
using HotelSector.ApplicationServices.RoomBooking;
using HotelSector.Models.Room;
using HotelSector.Models.RoomBooking;
using HotelSector.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSectorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpPost("GetAllRooms")]
        public ApiResponseDto GetAllRooms(RoomInputDto inputDto)
        { 
            return _roomService.GetAllRooms(inputDto);
        }

    }
}
