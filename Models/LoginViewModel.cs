using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        [MaxLength(10, ErrorMessage = "maz 20 characters allowed")]
        [DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "max 20 or min 5 characters allowed")]
        public string Password { get; set; }

    }
}
