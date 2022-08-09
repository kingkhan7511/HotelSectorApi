using HotelSector.Shared.User;

namespace HotelSector.Shared
{
    public class HotelSectorMessages
    {
        #region Validation
        public const string LoginInputRequired = "Please pass email and pin or refresh token";
        public static string LoginPinShouldNotBeLessThanSix = "Pin should not be less then " + UserStaticValue.LoginPinMaxMinLength.ToString();
        public const string StartDateShouldBeGreaterThanNow = "Start date time should be greater then current date time";
        public const string EndDateShouldBeGreaterThanStartDate = "End date time should be greater then start date time";
        public const string ShouldBe1HrDifference = "Should be 1 hour difference to book the room.";
        public const string IdShouldNotLessThan1 = "Id should not be less than one but it can be null.";


        #endregion

        public const string ExceptionMessage = "Internal server error occured, please see the details error.";
        public const string UserNotFound = "Invalid Email, no user record found.";
        public const string InvalidUserOtpNotFound = "Invalid Email and Pin, No user record found.";
        public const string OtpEmailSent = "OTP has been sent to the email, please use Login api to get authorized.";
        public const string LoginSucessfully = "The user has been logged in sucessfully.";
        public const string InvalidTokenOrRefreshToken = "The token or refresh token is invalid.";
        public const string InvalidEmailAddress = "The input email is not valid.";
        public const string RoomHasBeenBooked = "Room has been booked.";
        public const string BookingHasBeenUpdate = "Room booking has been updated.";
        public const string UnAuthorize = "Your are unathorize to use the API, please refresh your token or get new token by using login.";
        public const string RoomIsNotAvailable = "We are sorry, the room is not available on mentioned dates.";
        public const string RoomIsNotBookedByYou = "The entered information is invalid or not booked by you.";
        public const string InValidRoomId = "Room Id is invalid.";
        public const string RoomBookingDelete = "Room Booking has been deleted sucessfully";
        public const string InvalidRoomBooking = "The provided information is wrong.";
    }
}
