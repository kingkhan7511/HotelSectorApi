using HotelSector.Models.Jwt;

namespace HotelSector.Models.Users
{
    public class LoginOutput
    {
        public long Id { get; set; }
        public Tokens Token { get; set; }
    }
}
