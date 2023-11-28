using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HOSPITALMANAGEMENT.Model.DbModels;

namespace HOSPITALMANAGEMENT.Model
{
    public class CartModel
    {
        public int Id { get; set; }

        // Removed [Required] because it's not strictly required for every operation
        public string? Name { get; set; }


        public string? ImageFile { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Foreign key for user
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Navigation property for user
        public User User { get; set; }
    }
}
