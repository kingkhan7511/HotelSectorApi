using HotelSector.Core.Entities.Token;
using HotelSector.Domain.Base;

namespace HotelSector.Domain.Token
{
    public interface IUserRefreshTokenRepository: IBaseRepository<UserRefreshTokensEntity>
    {
        UserRefreshTokensEntity GetByToken(string token);
        void InsertOrUpdate(long userId, string refreshToken);
    }
}
