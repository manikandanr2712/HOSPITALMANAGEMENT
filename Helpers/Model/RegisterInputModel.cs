using System.ComponentModel.DataAnnotations;

namespace HOSPITALMANAGEMENT.Model
{
    public class RegisterInputModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmedPassword { get; set; }
        
        }
}
