using HotelSector.Models.Jwt;
using HotelSector.Models.Users;

namespace HotelSector.Domain.Jwt
{
    public interface IJWTManagerRepository
    {
        Tokens GetToken(long userId, string email);
        LoginOutput RefreshToken(long userId, string refreshToken);
    }
}
