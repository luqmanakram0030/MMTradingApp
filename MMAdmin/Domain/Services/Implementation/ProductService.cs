

namespace MMAdmin.Domain.Services.Implementation;


public class ProductService : IProductService
{
    private readonly FirebaseClient _firebaseClient;

    public ProductService()
    {
        _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return (await _firebaseClient
            .Child("Products")
            .OnceAsync<Product>())
            .Select(item => new Product
            {
                Id = item.Object.Id,
                Name = item.Object.Name,
                Description = item.Object.Description,
                Price = item.Object.Price,
                StockQuantity = item.Object.StockQuantity,
                Category = item.Object.Category,
                ImageUrl = item.Object.ImageUrl
            }).ToList();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var allProducts = await GetAllProductsAsync();
        return allProducts.FirstOrDefault(p => p.Id == id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _firebaseClient
            .Child("Products")
            .PostAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        var toUpdateProduct = (await _firebaseClient
            .Child("Products")
            .OnceAsync<Product>())
            .FirstOrDefault(a => a.Object.Id == product.Id);

        await _firebaseClient
            .Child("Products")
            .Child(toUpdateProduct.Key)
            .PutAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var toDeleteProduct = (await _firebaseClient
            .Child("Products")
            .OnceAsync<Product>())
            .FirstOrDefault(a => a.Object.Id == id);

        await _firebaseClient
            .Child("Products")
            .Child(toDeleteProduct.Key)
            .DeleteAsync();
    }
}

