using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelSector.ApplicationServices.Session
{
    public class HotelSectorSession:IHotelSectorSession
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HotelSectorSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? GetUserId() 
        {
           
            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
            long? currentUserId = null;
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                currentUserId = long.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            return currentUserId;
        }
    }
}
