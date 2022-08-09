using HostelSector.Models.Base;
using HotelSector.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSector.ApplicationServices.Room
{
    public interface IRoomService
    {
        ApiResponseDto GetAllRooms(RoomInputDto inputDto);
    }
}
