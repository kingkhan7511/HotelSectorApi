using HostelSector.Models.Base;
using HotelSector.Models.RoomBooking;

namespace HotelSector.ApplicationServices.RoomBooking
{
    public interface IRoomBookingService
    {
        ApiResponseDto AddUpdateBooking(RoomBookingInputDto inputDto);
        ApiResponseDto GetMyBookedRooms(GetMyBookedRoomsInputDto inputDto);
        ApiResponseDto DeleteBooking(RoomBookingDeleteInputDto inputDto);
    }
}
