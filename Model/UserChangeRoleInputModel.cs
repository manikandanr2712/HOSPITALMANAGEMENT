using System.ComponentModel.DataAnnotations;

namespace HOSPITALMANAGEMENT.Model
{
    public class UserChangeRoleInputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
