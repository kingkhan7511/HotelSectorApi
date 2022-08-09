using HotelSector.Core.EntityFrameworkCore.Contexts;
using HotelSector.Domain.Room;
using HotelSector.Domain.RoomBooking;
using HotelSector.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSectorDataAccess.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelSectorDbContext _context;
        public UnitOfWork(HotelSectorDbContext context)
        {
            _context = context;
            Users = new UsersRepository(_context);
            Room = new RoomRepository(_context);
            RoomBooking = new RoomBookingRepository(_context);
            
        }
        public IUsersRepository Users { get; private set; } 
        public IRoomRepository Room { get; private set; }
        public IRoomBookingRepository RoomBooking { get; private set; } 
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
