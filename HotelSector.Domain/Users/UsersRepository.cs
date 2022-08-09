using HotelSector.Core.Entities.User;
using HotelSector.Core.EntityFrameworkCore.Contexts;
using HotelSector.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSector.Domain.Users
{
   public  class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
    {
        public UsersRepository(HotelSectorDbContext context) : base(context)
        {

        }

        public UserEntity Authenticate(string email, string otp)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email&& x.LoginPin == otp);
        }

        public UserEntity GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
