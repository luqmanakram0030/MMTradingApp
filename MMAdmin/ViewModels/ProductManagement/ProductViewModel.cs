using MMAdmin.Abstract;
using MMAdmin.Views.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMAdmin.ViewModels.ProductManagement
{
    public partial class ProductViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<Product> _FilteredList;
        private ObservableCollection<Product> _UnfilteredList;
        private string _searchText;
        #endregion
        #region Properties
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.Execute(null);
            }
        }
        #endregion
        public ICommand SearchCommand { get; set; }
        public IAsyncRelayCommand LoadProductCommand { get; }
        private readonly ISharedService _sharedService;
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product> products;
        public ProductViewModel(ISharedService sharedService, IProductService productService)
        {
            _productService = productService;
            this._sharedService = sharedService;
            Products = new ObservableCollection<Product>();
            LoadProductCommand = new AsyncRelayCommand(LoadProductAsync);
            SearchCommand = new Command(async () => await PerformSearch());
        }
        #region Methods
        public async Task LoadProductAsync()
        {
            try
            {
                Common.BusyIndicator(true);
                Products.Clear();
                var products = await _productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        product.ProductImage = product.ProductImage is ImageSource;
                        byte[] base64Stream = Convert.FromBase64String(product.ImageUrl);
                        product.ProductImage = ImageSource.FromStream(() => new MemoryStream(base64Stream));
                    }
                    Products.Add(product);
                }
                _UnfilteredList = Products;
                Common.BusyIndicator(false);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(this._searchText))
            {
                Products = _UnfilteredList;
            }
            else
            {
                string searchTextLower = _searchText.ToLower();
                _FilteredList = new ObservableCollection<Product>(
                _UnfilteredList.Where(i =>
                    i.Name.ToLower().Contains(searchTextLower) ||
                    i.Category.ToLower().Contains(searchTextLower)));

                Products = _FilteredList;
            }
        }


        [RelayCommand]
        private async Task NavigateToAddProduct(Product product)
        {
            _sharedService.Add<Product>("SelectedProduct", product);
            await Shell.Current.GoToAsync(nameof(AddProductView));
        }
        #endregion
    }
}
