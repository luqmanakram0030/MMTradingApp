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
        partial void OnSelectedCategoryChanged(Category value)
        {
            // Logic to set SelectedProduct based on SelectedCategory
            if (SelectedCategory.Name != null)
                SelectedProduct.Category = SelectedCategory.Name;
        }
        [ObservableProperty]
        private ObservableCollection<Category> categories;
        public AddProductViewModel(ISharedService sharedService, IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            this._sharedService = sharedService;
            SelectedCategory = new Category();
            Categories = new ObservableCollection<Category>();
            _ = LoadCategoryAsync();
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

        }

        [RelayCommand]
        private async Task GoBackAsync(Object obj)
        {
            try
            {
                var page = obj as AddProductView;
                var btnAddEmployee = page.FindByName("btngoback");


                await Common.ControlBounceEffect(btnAddEmployee);
                await Shell.Current.GoToAsync("../..");
                // await Shell.Current.GoToAsync("..");

            }
            catch (Exception ex)
            {
                Common.BusyIndicator(false);
            }
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
        [RelayCommand]
        private async Task AddProductAsync(Object obj)
        {
            try
            {
                var page = obj as AddProductView;
                var btnAddProduct = page.FindByName("btnAddProduct");
                if (!ValidationHelper.IsFormValid(SelectedProduct, page))
                {
                    return;
                }

                await Common.ControlBounceEffect(btnAddProduct);
                if (SelectedProduct == null)
                    return;
                SelectedProduct.Category = SelectedCategory.Name;
                if (SelectedProduct.Id.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    await _productService.UpdateProductAsync(SelectedProduct);
                    await Shell.Current.GoToAsync("../..");
                }
                else
                {
                    await _productService.AddProductAsync(SelectedProduct);
                    await Shell.Current.GoToAsync("../..");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

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
                // Get the image content as a byte array




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