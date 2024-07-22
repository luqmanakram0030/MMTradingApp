
namespace MMAdmin.Domain.Models;

public class Category
{
    // Unique identifier for the category
    public int Id { get; set; }

    // Name of the category
    public string Name { get; set; }

    // Description of the category
    public string Description { get; set; }

    // List of products in this category
    public List<Product> Products { get; set; }

    // Constructor to initialize the category
    public Category()
    {
       
        Products = new List<Product>();
    }

    // Method to add a product to the category
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    // Method to remove a product from the category
    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
    }

    // Method to display category details (optional)
    public override string ToString()
    {
        return $"Category ID: {Id}, Name: {Name}, Description: {Description}, Number of Products: {Products.Count}";
    }
}

