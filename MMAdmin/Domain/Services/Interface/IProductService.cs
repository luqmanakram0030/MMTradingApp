
namespace MMAdmin.Domain.Services.Interface;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
    Task<byte[]> ProcessMediaAsync(Stream stream);
    Task<string> CapturePhotoAsync();
    Task<string> PickPhotoAsync();
    Task RequestPermissionAsync();
}

