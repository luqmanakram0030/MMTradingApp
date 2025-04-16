using System.ComponentModel.DataAnnotations;
namespace MMAdmin.Domain.Models
{
    public class ShopModel
    {
        public Guid ShopId { get; set; }

        [Required(ErrorMessage = "Shop name is required")]
        [StringLength(100, ErrorMessage = "Shop name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Owner name is required")]
        [StringLength(50, ErrorMessage = "Owner name cannot exceed 50 characters")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; } // Renamed for consistency

        
        public PlaceDetails Location { get; set; } // Use the same type as EmployeeModel
    }

    
}
