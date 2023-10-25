using System.ComponentModel.DataAnnotations;

namespace HOSPITALMANAGEMENT.Model
{
    public class UserEventInputModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string EventId { get; set; }
    }
}
