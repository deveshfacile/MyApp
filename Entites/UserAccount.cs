using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Entites
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "maz 50 characters allowed")]
        public  string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "maz 50 characters allowed")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        //[DataType(DataType.EmailAddress)]
        [MaxLength(20, ErrorMessage = "maz 20 characters allowed")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(10, ErrorMessage = "maz 20 characters allowed")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "maz 16 characters allowed")]
        public string Password { get; set; }

      

       

        
    }
}
