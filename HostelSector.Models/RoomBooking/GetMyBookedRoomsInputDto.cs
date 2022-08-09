using HotelSector.Models.Base;
using System;

namespace HotelSector.Models.RoomBooking
{
    public class GetMyBookedRoomsInputDto : PagenationBaseModel
    { 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
