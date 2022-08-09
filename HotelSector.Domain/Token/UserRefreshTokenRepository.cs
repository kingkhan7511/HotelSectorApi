using HotelSector.Core.Entities.Token;
using HotelSector.Core.EntityFrameworkCore.Contexts;
using HotelSector.Domain.Base;
using System;
using System.Linq;

namespace HotelSector.Domain.Token
{
    public class UserRefreshTokenRepository : BaseRepository<UserRefreshTokensEntity>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(HotelSectorDbContext context) : base(context)
        {

        }

        public UserRefreshTokensEntity GetByToken(string token)
        {
            return _context.UserRefreshTokens.Where(x => x.RefreshToken == token).FirstOrDefault();
        }

        public void InsertOrUpdate(long userId, string refreshToken)
        {
            var entity = _context.UserRefreshTokens.Where(x => x.UserId == userId).FirstOrDefault();
            if (entity == null)
            {
                entity = new UserRefreshTokensEntity()
                {
                    RefreshToken = refreshToken,
                    UserId = userId,
                    CreatedBy = userId
                };
                _context.UserRefreshTokens.Add(entity);
            }
            else
            {
                entity.RefreshToken = refreshToken;
                entity.EditBy = userId;
                entity.EditDateTime = DateTime.Now;
            }
        }
    }
}
