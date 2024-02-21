using System.ComponentModel.DataAnnotations.Schema;

using HOSPITALMANAGEMENT.Model.DbModels;

namespace HOSPITALMANAGEMENT.Model
{
    public class shippingAddress
    {
        public int ShippingAddressId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        // Foreign key for user
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Navigation property for user
        public User User { get; set; }
    }
}
