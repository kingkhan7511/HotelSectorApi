using HotelSector.Domain.Room;
using HotelSector.Domain.RoomBooking;
using HotelSector.Domain.Users;
using HotelSector.Models.RoomBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSectorDataAccess.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; } 
        IRoomRepository Room { get; } 
        IRoomBookingRepository RoomBooking { get; } 
        int Complete();
    }
}
