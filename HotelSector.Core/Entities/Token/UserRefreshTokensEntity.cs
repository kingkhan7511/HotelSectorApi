using HotelSector.Core.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSector.Core.Entities.Token
{
    public class UserRefreshTokensEntity : BaseEntity
    {
        public string RefreshToken { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity UserFK { get; set; }
    }
}
