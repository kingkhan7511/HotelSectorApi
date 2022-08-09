using HotelSector.Shared;
using HotelSector.Shared.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelSector.Models.Users
{
    public class LoginInputDto : IValidatableObject
    {
        [StringLength(UserStaticValue.EmailMaxLength)]

        public string Email { get; set; }
        [StringLength(UserStaticValue.LoginPinMaxMinLength)]
        public string Pin { get; set; }
        [StringLength(UserStaticValue.TokenMaxLength)]
        public string RefreshToken { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if ((string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Pin)) && string.IsNullOrEmpty(RefreshToken))
            {
                yield return new ValidationResult(HotelSectorMessages.LoginInputRequired);
            }

            else if (!string.IsNullOrEmpty(Email) && !UtilitiesFunctions.IsValidEmail(Email))
            {
                yield return new ValidationResult(HotelSectorMessages.InvalidEmailAddress);
            }
            else if (!string.IsNullOrEmpty(Pin) && Pin.Length < UserStaticValue.LoginPinMaxMinLength)
            {
                yield return new ValidationResult(HotelSectorMessages.LoginPinShouldNotBeLessThanSix);
            }

        }
    }
}
