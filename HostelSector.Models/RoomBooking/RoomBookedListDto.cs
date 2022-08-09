using HotelSector.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSector.Models.RoomBooking
{
   public  class RoomBookedListDto: RoomBookingInputDto
    {
        public long Id { get; set; }
        public RoomListOutDto RoomDetails { get; set; }
    }
}
