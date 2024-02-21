using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HOSPITALMANAGEMENT.Model
{
    public class ProductModel
    {
        // Removed [Key] since @ProductId from stored procedure handles it
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Removed [Required] because it's not strictly required for every operation
        public string? Name { get; set; }

        // Nullable since stored procedure allows null for @ImageFile
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be non-negative.")]
        public int? StockQuantity { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

}
