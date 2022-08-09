using System;

namespace HotelSector.Models.Jwt
{
    public class Tokens
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; } 
        public string RefreshToken { get; set; }
        public const string TokenType = "Bearer";
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
