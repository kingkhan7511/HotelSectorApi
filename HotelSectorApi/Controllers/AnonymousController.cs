using HostelSector.Models.Base;
using HotelSector.ApplicationServices.Users;
using HotelSector.Models.Users;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSectorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly IUserService _userService;
        public AnonymousController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("LoginToValidateEmail")]
        public ApiResponseDto LoginToValidateEmail(LoginValidateInputDto inputDto)
        {
            return _userService.LoginToValidateEmail(inputDto);

        }
        [HttpPost("Authenticate")]
        public ApiResponseDto Authenticate(LoginInputDto inputDto)
        {
            return _userService.Authenticate(inputDto);
        }
    }
}
