using HostelSector.Models.Base;
using HotelSector.Models.Base;
using HotelSector.Models.Room;
using HotelSector.Shared;
using HotelSectorDataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSector.ApplicationServices.Room
{
   public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork; 
        public RoomService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork; 
        }

        public ApiResponseDto GetAllRooms(RoomInputDto inputDto)
        {
            ApiResponseDto responseDto = new();

            try
            {
                ResultListDto resultListDto = new();
                var query = _unitOfWork.Room.GetAll();
                if (!string.IsNullOrEmpty(inputDto.Filter))
                {
                    query = query.Where(x=>x.RoomNo == inputDto.Filter);
                }
                resultListDto.Count = query.Count();
                resultListDto.Result = query.Skip(inputDto.Skip).Take(inputDto.MaxResultCount)
                    .Select(x=> new RoomListOutDto()
                    {
                        
                        Id = x.Id, 
                        IsAvailable   = x.IsAvailable, 
                        RoomFloor = x.RoomFloor, 
                        RoomNo = x.RoomNo,
                        RentPerHour = x.RentPerHour, 
                    }
                ).ToList();
                return responseDto.SucessResponse(string.Empty, resultListDto);
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }
        }
    }
}
