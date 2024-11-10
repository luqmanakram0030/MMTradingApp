

using MMAdmin.Views.ProductManagement;

namespace MMAdmin.ViewModels.ProductManagement;

public partial class ProductDetailViewModel : ObservableObject
    {
        public IAsyncRelayCommand UpdateProductCommand { get; }
        public IAsyncRelayCommand DeleteProductCommand { get; }

        private readonly ISharedService _sharedService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        private bool isVisible;
        [ObservableProperty]
        private bool isImageVisible;
        [ObservableProperty]
        private ImageSource productImage;

        [ObservableProperty]
        private Product selectedProduct;
        [ObservableProperty]
        private Category selectedCategory;
        [ObservableProperty]
        private ObservableCollection<Category> categories;

        public ProductDetailViewModel(ISharedService sharedService, IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sharedService = sharedService;

            SelectedCategory = new Category();
            Categories = new ObservableCollection<Category>();
            SelectedProduct = _sharedService.GetValue<Product>("SelectedProduct");

            if (SelectedProduct != null)
            {
                SelectedCategory.Name = SelectedProduct.Category;
                IsVisible = true;
                IsImageVisible = true;
                if (!string.IsNullOrEmpty(SelectedProduct.ImageUrl))
                {
                    byte[] base64Stream = Convert.FromBase64String(SelectedProduct.ImageUrl);
                    ProductImage = ImageSource.FromStream(() => new MemoryStream(base64Stream));
                }
            }
            else
            {
                SelectedProduct = new Product();
                IsVisible = false;
                IsImageVisible = false;
            }

            // Initialize commands
            UpdateProductCommand = new AsyncRelayCommand(UpdateProductAsync);
            DeleteProductCommand = new AsyncRelayCommand(DeleteProductAsync);
        }

        public async Task LoadCategoryAsync()
        {
            try
            {
                Categories.Clear();
                var categories = await _categoryService.GetAllCategoriesAsync();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        private async Task UpdateProductAsync()
        {
            if (SelectedProduct == null)
                return;

            SelectedProduct.Category = SelectedCategory.Name;

            if (SelectedProduct.Id != Guid.Empty)
            {
                await _productService.UpdateProductAsync(SelectedProduct);
            }
            else
            {
                await _productService.AddProductAsync(SelectedProduct);
            }
        }

        private async Task DeleteProductAsync()
        {
            try
            {
                await _productService.DeleteProductAsync(SelectedProduct.Id);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

       

        
       
        [RelayCommand]
        private async Task GoBackAsync(Object obj)
        {
            try
            {
                var page = obj as ProductDetailView;
                var btnAddEmployee = page.FindByName("btngoback");
                

                await Common.ControlBounceEffect(btnAddEmployee);

                await Shell.Current.GoToAsync("..");
                
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }
     [RelayCommand]
        private async Task EditProductAsync(Object obj)
        {
            try
            {
                var page = obj as ProductDetailView;
                var btnAddEmployee = page.FindByName("btnedit");
                

                await Common.ControlBounceEffect(btnAddEmployee);
                _sharedService.Add<Product>("SelectedProduct", SelectedProduct);
                await Shell.Current.GoToAsync(nameof(AddProductView));
                
            }
            catch(Exception ex)
            {
              //  Common.BusyIndicator(false);
            }
        }
    }