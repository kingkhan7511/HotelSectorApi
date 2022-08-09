using HotelSector.Shared.User;
using System.ComponentModel.DataAnnotations;

namespace HotelSector.Models.Users
{
    public class LoginValidateInputDto
    {
        [Required]
        [StringLength(UserStaticValue.EmailMaxLength)]
        public string Email { get; set; }
    }
}
