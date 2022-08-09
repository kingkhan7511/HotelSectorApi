using HotelSector.Core.Entities.RoomBooking;
using HotelSector.Domain.Base;
using HotelSector.Models.Base;
using HotelSector.Models.RoomBooking;

namespace HotelSector.Domain.RoomBooking
{
    public interface IRoomBookingRepository : IBaseRepository<RoomBookingEntity>
    {
        void BookRoom(long currentUserId,  RoomBookingInputDto inputDto);
        bool CheckRoomAvailabilty(RoomBookingInputDto inputDto, bool isForUpdate);
        ResultListDto GetMyBookedRoom(long currentUserId, GetMyBookedRoomsInputDto inputDto);
    }
}
