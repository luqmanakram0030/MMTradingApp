using MMAdmin.Abstract;

using MMAdmin.Views.Popup;
using MMAdmin.Views.ProductManagement;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.ViewModels.ProductManagement
{
    public partial class AddProductViewModel : ObservableObject
    {
        public IAsyncRelayCommand AddProductCommand { get; }
        public IAsyncRelayCommand DeleteProductCommand { get; }

        private readonly ISharedService _sharedService;

        [ObservableProperty]
        private bool isVisible;
        [ObservableProperty]
        private bool isImageVisible;
        [ObservableProperty]
        private ImageSource productImage;

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        private Product selectedProduct;
        [ObservableProperty]
        private Category selectedCategory;
        [ObservableProperty]
        private ObservableCollection<Category> categories;
        public AddProductViewModel(ISharedService sharedService, IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            this._sharedService = sharedService;
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
            AddProductCommand = new AsyncRelayCommand(AddProductAsync);
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

            }
        }
        private async Task AddProductAsync()
        {
            if (SelectedProduct == null)
                return;
            SelectedProduct.Category = SelectedCategory.Name;
            if (SelectedProduct.Id != Guid.Empty)
            {
                await _productService.UpdateProductAsync(SelectedProduct);
            }
            else
                await _productService.AddProductAsync(SelectedProduct);
        }
        private async Task DeleteProductAsync()
        {
            await MopupService.Instance.PushAsync(new DeleteOnlyPopup(SelectedProduct.Id));
            MessagingCenter.Subscribe<string>(this, SelectedProduct.Id.ToString(), async (sender) =>
            {
                if (sender == "Delete")
                {
                    await _productService.DeleteProductAsync(SelectedProduct.Id);

                }
                MessagingCenter.Unsubscribe<string>(this, "Delete");
            });
        }
        [RelayCommand]
        public async Task AddImage()
        {
            await _productService.RequestPermissionAsync();
            var result = await App.Current.MainPage.DisplayActionSheet("Change Picture", "Cancel", null, "Take Photo", "Select Photo");
            if (result == "Take Photo")
            {
                SelectedProduct.ImageUrl = await _productService.CapturePhotoAsync();
            }
            else if (result == "Select Photo")
            {
                SelectedProduct.ImageUrl = await _productService.PickPhotoAsync();
            }
            if (!string.IsNullOrWhiteSpace(SelectedProduct.ImageUrl))
            {
                byte[] base64Stream = Convert.FromBase64String(SelectedProduct.ImageUrl);
                ProductImage = ImageSource.FromStream(() => new MemoryStream(base64Stream));
                IsImageVisible = true;
            }
        }
        [RelayCommand]
        public async Task NavigatetoCategory()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CategoryView));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
