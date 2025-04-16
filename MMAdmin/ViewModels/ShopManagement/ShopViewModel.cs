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

        public ICommand OpenDialer {  get; set; }
        public ICommand OpenWatsapp { get; set; }
       

        public ShopViewModel(ISharedService sharedService, IShopService shopService)
        {
            _shopService = shopService;
            this._sharedService = sharedService;
            Shops = new ObservableCollection<ShopModel>();
            LoadShopsCommand = new AsyncRelayCommand(LoadShopsAsync);
            SearchCommand = new Command(async () => await PerformSearch());

            OpenDialer = new Command<string>(async (phoneNumber) =>
            {
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    PhoneDialer.Open(phoneNumber);
                }
            });

            OpenWatsapp = new Command<string>(async (phoneNumber) =>
            {
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    // Step 1: Remove dashes, spaces, etc.
                    /* "0300-9400007" 
                       "0300 9400007"
                       " 03009400007 " 
                    */
                    var cleaned = phoneNumber.Replace("-", "").Replace(" ", "").Trim();

                    // Step 2: Remove leading zero (if any), then add 92
                    // "03009400007" becomes "92 3214330988"

                    if (cleaned.StartsWith("0"))
                    {
                        cleaned = "92" + cleaned.Substring(1);
                    }
                    else if (cleaned.StartsWith("+"))
                    {
                        cleaned = cleaned.Replace("+", "");
                    }

                    // Step 3: Create WhatsApp URL
                    var uri = new Uri($"https://wa.me/{cleaned}");
                    await Launcher.OpenAsync(uri);
                }
            });
            
        }
        #region Methods
        [RelayCommand]
        private async Task GoBackAsync(Object obj)
        {
            try
            {
                var page = obj as AddShop;
                var btnAddEmployee = page.FindByName("btngoback");
                

                await Common.ControlBounceEffect(btnAddEmployee);
                await Shell.Current.GoToAsync("..");
                // await Shell.Current.GoToAsync("..");
                
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }
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
