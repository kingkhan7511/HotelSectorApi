using HotelSector.Domain.Token;
using HotelSector.Domain.Users;
using HotelSector.Models.Jwt;
using HotelSector.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HotelSector.Domain.Jwt
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        private readonly IUsersRepository _usersRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly double tokenExpiryInMints;
        private readonly int refreshTokenValidityInDays;
        public JWTManagerRepository(IConfiguration iconfiguration, IUsersRepository usersRepository,
            IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            this.iconfiguration = iconfiguration;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _usersRepository = usersRepository;
            _ = double.TryParse(iconfiguration["JWT:ExpiryInMints"], out tokenExpiryInMints);
            _ = int.TryParse(iconfiguration["JWT:RefreshTokenValidityInDays"], out refreshTokenValidityInDays);
        }
        public Tokens GetToken(long userId, string email)
        {
            string token = GenerateToken(userId, email, false);
            //var refreshToken = GenerateRefreshToken();
            var refreshToken = GenerateToken(userId, email, true);
            _userRefreshTokenRepository.InsertOrUpdate(userId, refreshToken);

            return new Tokens
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiryTime = DateTime.Now.AddMinutes(tokenExpiryInMints),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays),
            };

        }

        public LoginOutput RefreshToken(long userId, string refreshToken)
        {
            var entity = _userRefreshTokenRepository.GetByToken(refreshToken);
            if (entity == null)
            {
                return null;
            }
            var principal = GetPrincipalFromExpiredToken(refreshToken);
            if (principal == null)
            {
                return null;
            }
            if (!principal.Claims.Any())
            {
                return null;
            }
            Claim emailClaim = principal.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault();
            if (emailClaim == null)
            {
                return null;
            }
            Claim refreshTokenClaim = principal.Claims.Where(c => c.Type == "Refresh").FirstOrDefault();
            if (refreshTokenClaim == null)
            {
                return null;
            }
            bool isRefreshToken = bool.Parse(refreshTokenClaim.Value);
            if (!isRefreshToken)
            {
                return null;
            }

            Claim nameIdentifierClaim = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();
            if (nameIdentifierClaim == null)
            {
                return null;
            }
            string email = emailClaim.Value;
            // compare the id of both token 
            long id = long.Parse(nameIdentifierClaim.Value);
            if (id != entity.Id)
            {
                return null;
            }
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            // Validate user Email 
            var user = _usersRepository.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            string token = GenerateToken(userId, email, false);
            //refreshToken = GenerateRefreshToken();
            refreshToken = GenerateToken(userId, email, true);

            _userRefreshTokenRepository.InsertOrUpdate(userId, refreshToken);
            return new LoginOutput()
            {
                Id = user.Id,
                Token = new()
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiryTime = DateTime.Now.AddMinutes(tokenExpiryInMints),
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays),
                }
            };
        }

        private string GenerateToken(long userId, string email, bool isForRefresh)
        {
            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email,  email),
                    new Claim("Refresh", isForRefresh.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                }),
                // token need to expire base on app setting value which is in mintues.  
                Expires = isForRefresh ? DateTime.UtcNow.AddDays(refreshTokenValidityInDays) : DateTime.UtcNow.AddMinutes(tokenExpiryInMints),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token = null)
        {
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:SecretKey"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = iconfiguration["JWT:Issuer"],
                ValidAudience = iconfiguration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
