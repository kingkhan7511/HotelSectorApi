using HotelSector.Shared.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSector.Core.Entities.User
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {
        [Required]
        [StringLength(UserStaticValue.EmailMaxLength)]
        public string Email { get; set; }
        [StringLength(UserStaticValue.LoginPinMaxMinLength)]
        public string LoginPin { get; set; }
        [Required]
        [StringLength(UserStaticValue.FirstNameMaxLength, MinimumLength = UserStaticValue.FirstNameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(UserStaticValue.LastNameMaxLength, MinimumLength = UserStaticValue.LastNameMinLength)]
        public string LastName { get; set; }
        [StringLength(UserStaticValue.PhoneMaxLength)]
        public string PhoneNumber { get; set; }

      
    }
}
