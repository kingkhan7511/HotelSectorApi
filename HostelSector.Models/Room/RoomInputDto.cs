using HotelSector.Models.Base;

namespace HotelSector.Models.Room
{
    public class RoomInputDto : PagenationBaseModel
    {
        public string Filter { get; set; }
    }
}
