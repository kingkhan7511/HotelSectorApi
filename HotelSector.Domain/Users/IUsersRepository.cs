using HotelSector.Core.Entities.User;
using HotelSector.Domain.Base;

namespace HotelSector.Domain.Users
{
    public interface IUsersRepository : IBaseRepository<UserEntity>
    {
        UserEntity GetUserByEmail(string email);
        UserEntity Authenticate(string email, string otp);
    }
}
