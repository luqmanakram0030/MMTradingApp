using MMAdmin.Abstract;
using MMAdmin.Views.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMAdmin.ViewModels.ShopManagement
{
    public partial class ShopViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<ShopModel> _FilteredList;
        private ObservableCollection<ShopModel> _UnfilteredList;
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
        public IAsyncRelayCommand LoadShopsCommand { get; }

        private readonly ISharedService _sharedService;

        private readonly IShopService _shopService;

        [ObservableProperty]
        private ObservableCollection<ShopModel> shops;

        public ShopViewModel(ISharedService sharedService, IShopService shopService)
        {
            _shopService = shopService;
            this._sharedService = sharedService;
            Shops = new ObservableCollection<ShopModel>();
            LoadShopsCommand = new AsyncRelayCommand(LoadShopsAsync);
            SearchCommand = new Command(async () => await PerformSearch());
        }
        #region Methods
        public async Task LoadShopsAsync()
        {
            try
            {
                Common.BusyIndicator(true);
                Shops.Clear();
                var shops = await _shopService.GetAllShopsAsync();
                foreach (var shop in shops)
                {
                    Shops.Add(shop);
                }
                _UnfilteredList = Shops;

                Common.BusyIndicator(false);
            }
            catch (Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }
        public async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(this._searchText))
            {
                Shops = _UnfilteredList;
            }
            else
            {
                string searchTextLower = _searchText.ToLower();
                _FilteredList = new ObservableCollection<ShopModel>(
                _UnfilteredList.Where(i =>i.Name.ToLower().Contains(searchTextLower)));
                Shops = _FilteredList;
            }
        }

        [RelayCommand]
        private async Task NavigateToAddShop(ShopModel shop)
        {
            _sharedService.Add<ShopModel>("SelectedShop", shop);
            await Shell.Current.GoToAsync(nameof(AddShop));
        }
        [RelayCommand]
        private async Task NavigateToShopDetail(ShopModel shop)
        {
            _sharedService.Add<ShopModel>("SelectedShop", shop);
            await Shell.Current.GoToAsync(nameof(ShopDetailView));
        }
        #endregion
    }
}
