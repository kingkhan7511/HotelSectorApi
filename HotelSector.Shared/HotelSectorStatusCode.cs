namespace HotelSector.Shared
{
    public class HotelSectorStatusCode
    {
        public const int Sucess = 200;
        public const int InternalServiceError = 500;
        public const int NotFound = 404;
        public const int InvalidTokenOrRefreshToken = 101;
        public const int RoomIsNotAvailable = 102;
        public const int RoomIsNotBookedByYou = 103;
        public const int InValidRoomId = 104;
        public const int InValidRoomBookingId= 105;
        public const int UnAthorized = 401;
    }
}
