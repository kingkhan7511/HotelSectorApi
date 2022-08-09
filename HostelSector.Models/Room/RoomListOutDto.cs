namespace HotelSector.Models.Room
{
    public class RoomListOutDto
    {
        public long Id { get; set; }
        public string RoomNo { get; set; }
        public int RoomFloor { get; set; }
        public double RentPerHour { get; set; }
        public bool IsAvailable { get; set; }
    }
}
