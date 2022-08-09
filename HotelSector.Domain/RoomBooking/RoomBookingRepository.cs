using HotelSector.Core.Entities.RoomBooking;
using HotelSector.Core.EntityFrameworkCore.Contexts;
using HotelSector.Domain.Base;
using HotelSector.Models.Base;
using HotelSector.Models.Room;
using HotelSector.Models.RoomBooking;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelSector.Domain.RoomBooking
{
    public class RoomBookingRepository : BaseRepository<RoomBookingEntity>, IRoomBookingRepository
    {
        public RoomBookingRepository(HotelSectorDbContext context) : base(context)
        {

        }

        public void BookRoom(long currentUserId, RoomBookingInputDto inputDto)
        { 
            RoomBookingEntity roomBookingEntity = new()
            {
                RoomId = inputDto.RoomId,
                StartDate = inputDto.StartDate,
                EndDate = inputDto.EndDate,
                HowManyHours = inputDto.HowManyHours,
                TotalCharges = inputDto.TotalCharges,
                UserId = currentUserId
            };

            _context.RoomBooking.Add(roomBookingEntity);
        }
        public ResultListDto GetMyBookedRoom(long currentUserId, GetMyBookedRoomsInputDto inputDto)
        {

            var query = _context.RoomBooking.Include(x => x.RoomFK).Where(x => x.UserId == currentUserId);
            ResultListDto resultListDto = new();

            if (inputDto.StartDate != null)
            {
                query = query.Where(x => x.StartDate.Date >= inputDto.StartDate.Value.Date);
            }
            if (inputDto.EndDate != null)
            {
                query = query.Where(x => x.EndDate.Date <= inputDto.EndDate.Value.Date);
            }
            resultListDto.Count = query.Count();
            resultListDto.Result = query.Skip(inputDto.Skip)
                .Take(inputDto.MaxResultCount)
                .Select(x => new RoomBookedListDto()
                {
                    Id = x.Id,
                    EndDate = x.EndDate,
                    HowManyHours = x.HowManyHours,
                    RoomId = x.RoomId,
                    StartDate = x.StartDate,
                    TotalCharges = x.TotalCharges,
                    RoomDetails = new RoomListOutDto()
                    {
                        Id = x.RoomFK.Id,
                        IsAvailable = x.RoomFK.IsAvailable,
                        RentPerHour = x.RoomFK.RentPerHour,
                        RoomFloor = x.RoomFK.RoomFloor,
                        RoomNo = x.RoomFK.RoomNo
                    }
                }).ToList();
            return resultListDto;

        }
        public bool CheckRoomAvailabilty(RoomBookingInputDto inputDto, bool isForUpdate)
        {
            if (isForUpdate)
            {
                if (_context.RoomBooking.FirstOrDefault(x => x.Id != inputDto.Id &&
                      x.RoomId == inputDto.RoomId
                      && ((x.StartDate >= inputDto.StartDate && x.StartDate <= inputDto.EndDate)
                      || (x.EndDate >= inputDto.StartDate && x.EndDate <= inputDto.EndDate))
                      ) != null)
                {
                    return false;
                } 
            }
            else
            {
                if (_context.RoomBooking.FirstOrDefault(x => x.RoomId == inputDto.RoomId
                    && ((x.StartDate >= inputDto.StartDate && x.StartDate <= inputDto.EndDate)
                    || (x.EndDate >= inputDto.StartDate && x.EndDate <= inputDto.EndDate))
                    ) != null)
                {
                    return false;
                }

            }

            return true;
        }
    }
}
