using HotelSector.Shared;
using System;

namespace HostelSector.Models.Base
{
    public class ApiResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSucess { get; set; }
        public object Result { get; set; }
        public ErrorModel Error { get; set; }

        public ApiResponseDto ReturnInternalResponse(Exception ex)
        {
            StatusCode = HotelSectorStatusCode.InternalServiceError;
            Error = new ErrorModel()
            {
                ErrorMessage = HotelSectorMessages.ExceptionMessage,
                DetailedErrorMessage = ex == null ? "" : ex.ToString(),
            };
            IsSucess = false;
            Result = Array.Empty<object>();
            return this;
        }

         
        public ApiResponseDto SucessResponse(string message,object returnObject = null)
        {
            StatusCode = HotelSectorStatusCode.Sucess;
            Error = null;
            IsSucess = true;
            Message = message;
            Result = returnObject ?? Array.Empty<object>();
            return this;
        }
        public ApiResponseDto DynamicResponse(int statusCode, bool isSucess, ErrorModel errorModel = null, object result= null)
        {
            StatusCode = statusCode;
            Error = errorModel;
            IsSucess = isSucess;
            Result = result ?? Array.Empty<object>();
            return this;
        }

        public ApiResponseDto SucessResponse(object roomBookingDelete)
        {
            throw new NotImplementedException();
        }
    }
    public class ErrorModel
    {
        public string ErrorMessage { get; set; }
        public string DetailedErrorMessage { get; set; }
    }
}
