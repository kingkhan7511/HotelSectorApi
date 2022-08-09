using HostelSector.Models.Base;
using HotelSector.Core.Entities.User;
using HotelSector.Domain.Jwt;
using HotelSector.Models.Users;
using HotelSector.Shared;
using HotelSectorDataAccess.Base;
using System;

namespace HotelSector.ApplicationServices.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTManagerRepository _jwtManagerRepository;
        public UserService(IUnitOfWork unitOfWork,
            IJWTManagerRepository jwtManagerRepository)
        {
            _unitOfWork = unitOfWork;
            _jwtManagerRepository = jwtManagerRepository;
        }
        public ApiResponseDto Authenticate(LoginInputDto inputDto)
        {
            ApiResponseDto responseDto = new();
            try
            {
                if (!string.IsNullOrEmpty(inputDto.RefreshToken))
                {
                    // TODO: 
                    var loginOutput = _jwtManagerRepository.RefreshToken(1,inputDto.RefreshToken);
                    if (loginOutput == null)
                    {
                        return responseDto.DynamicResponse(HotelSectorStatusCode.InvalidTokenOrRefreshToken,
                           false,
                           new ErrorModel()
                           {
                               ErrorMessage = HotelSectorMessages.InvalidTokenOrRefreshToken,
                           });
                    }
                    else
                    {
                        _unitOfWork.Complete();
                        return responseDto.SucessResponse(HotelSectorMessages.LoginSucessfully, loginOutput);
                    }
                }
                UserEntity user = _unitOfWork.Users.Authenticate(inputDto.Email, inputDto.Pin);
                if (user != null)
                {
                    LoginOutput loginOutput = new()
                    {
                        Id = user.Id,
                        Token = _jwtManagerRepository.GetToken(user.Id,user.Email)
                    };
                    user.LoginPin = null;
                    _unitOfWork.Complete();
                    return responseDto.SucessResponse(HotelSectorMessages.LoginSucessfully, loginOutput);
                }
                else
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.NotFound,
                         false,
                         new ErrorModel()
                         {
                             ErrorMessage = HotelSectorMessages.UserNotFound,
                         });
                }
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }
        }

        public ApiResponseDto LoginToValidateEmail(LoginValidateInputDto inputDto)
        {
            ApiResponseDto responseDto = new();
            try
            {
                UserEntity user = _unitOfWork.Users.GetUserByEmail(inputDto.Email);
                if (user != null)
                {
                    // to assign dummy OTP for development 
                    // we can create manager to send OTP to user email address.
                    user.LoginPin = HotelSectorConst.DummyOTP;
                    _unitOfWork.Complete();
                    return responseDto.SucessResponse(HotelSectorMessages.OtpEmailSent, user.FirstName);
                }
                else
                {
                    return responseDto.DynamicResponse(HotelSectorStatusCode.NotFound,
                         false,
                         new ErrorModel()
                         {
                             ErrorMessage = HotelSectorMessages.UserNotFound,

                         });
                }
            }
            catch (Exception ex)
            {
                return responseDto.ReturnInternalResponse(ex);
            }

        }
    }
}
