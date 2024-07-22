
namespace MMAdmin.Domain.Models;

public class Product
{
    // Unique identifier for the product
    public int Id { get; set; }

    // Name of the product
    public string Name { get; set; }

    // Description of the product
    public string Description { get; set; }

    // Price of the product
    public decimal Price { get; set; }

    // Quantity available in stock
    public int StockQuantity { get; set; }

    // Category of the product
    public string Category { get; set; }

    // Image URL for the product
    public string ImageUrl { get; set; }

    // Constructor to initialize the product
    

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

