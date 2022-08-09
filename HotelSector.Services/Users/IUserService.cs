using HostelSector.Models.Base;
using HotelSector.Models.Users;

namespace HotelSector.ApplicationServices.Users
{
    public interface IUserService
    {
        ApiResponseDto Authenticate(LoginInputDto inputDto);
        ApiResponseDto LoginToValidateEmail(LoginValidateInputDto inputDto);
    }
}
