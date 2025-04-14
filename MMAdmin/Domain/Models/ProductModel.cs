using System;
using System.ComponentModel.DataAnnotations;

namespace MMAdmin.Domain.Models
{
    public class Product
    {
        // Unique identifier for the product
        [Key]
        public Guid Id { get; set; }

        // Name of the product
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        // Description of the product
        [StringLength(500)]
        public string Description { get; set; }

        // Price of the product
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        // Quantity available in stock
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        // Category of the product
        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        // Image URL for the product
        [Required]
        public string ImageUrl { get; set; }

        // Image Source for the product
        public object ProductImage { get; set; }

        // Constructor to initialize the product
        public Product()
        {
            // Id = Guid.NewGuid();
        }

        // Method to update the stock quantity
        public void UpdateStock(int quantity)
        {
            StockQuantity = quantity;
        }

        // Method to display product details (optional)
        public override string ToString()
        {
            return $"Product ID: {Id}, Name: {Name}, Description: {Description}, Price: {Price:C}, Stock: {StockQuantity}, Category: {Category}";
        }
    }
}