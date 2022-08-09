using HotelSector.Core.Entities.Room;
using HotelSector.Core.EntityFrameworkCore.Contexts;
using HotelSector.Domain.Base;
using System.Linq;

namespace HotelSector.Domain.Room
{
    public class RoomRepository : BaseRepository<RoomEntity>, IRoomRepository
    {
        public RoomRepository(HotelSectorDbContext context) : base(context)
        {

        }

        
    }
}
