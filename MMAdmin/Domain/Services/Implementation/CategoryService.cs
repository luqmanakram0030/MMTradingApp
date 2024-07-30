namespace MMAdmin.Domain.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly FirebaseClient _firebaseClient;
    private readonly IProductService _productService;

    public CategoryService(IProductService productService)
    {
        _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
        _productService = productService;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = (await _firebaseClient
            .Child("Categories")
            .OnceAsync<Category>())
            .Select(item => new Category
            {
                Id = item.Object.Id,
                Name = item.Object.Name,
                Description = item.Object.Description,
                Products = new List<Product>() // Initialize the list
            }).ToList();

        foreach (var category in categories)
        {
            // Fetch products for each category and add to the category's product list
            var products = await _productService.GetAllProductsAsync();
            category.Products.AddRange(products.Where(p => p.Category == category.Name));
        }

        return categories;
    }

    public async Task<Category> GetCategoryByIdAsync(Guid id)
    {
        var allCategories = await GetAllCategoriesAsync();
        return allCategories.FirstOrDefault(c => c.Id == id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        category.Id = Guid.NewGuid();
        await _firebaseClient
            .Child("Categories")
            .PostAsync(category);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var toUpdateCategory = (await _firebaseClient
            .Child("Categories")
            .OnceAsync<Category>())
            .FirstOrDefault(a => a.Object.Id == category.Id);

        await _firebaseClient
            .Child("Categories")
            .Child(toUpdateCategory.Key)
            .PutAsync(category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var toDeleteCategory = (await _firebaseClient
            .Child("Categories")
            .OnceAsync<Category>())
            .FirstOrDefault(a => a.Object.Id == id);

        await _firebaseClient
            .Child("Categories")
            .Child(toDeleteCategory.Key)
            .DeleteAsync();
    }
}

