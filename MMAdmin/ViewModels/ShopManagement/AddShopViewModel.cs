using MMAdmin.Abstract;
using MMAdmin.Views.Popup;
using MMAdmin.Views.ShopManagement;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.ViewModels.ShopManagement
{
    public partial class AddShopViewModel : ObservableObject
    {
        public IAsyncRelayCommand AddShopCommand { get; }
        public IAsyncRelayCommand DeleteShopCommand { get; }

        private readonly ISharedService _sharedService;
        private readonly HttpClient httpClient;
        private readonly FirebaseWebApi apiHelper;
        
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
                _ = SearchPlacesAsync();
            }
        }
        [ObservableProperty]
        private ObservableCollection<Place> places;
        [ObservableProperty]
        private PlaceDetails selectedPlaceDetails;

        [ObservableProperty]
        private bool isVisible;
        [ObservableProperty]
        private bool locationIsVisible;

        private readonly IShopService _shopService;

        [ObservableProperty]
        private ShopModel selectedShop;
        public AddShopViewModel(ISharedService sharedService, IShopService shopService)
        {
            SelectedPlaceDetails = new PlaceDetails();
            apiHelper = new FirebaseWebApi();
            LocationIsVisible = false;
            _shopService = shopService;
            this._sharedService = sharedService;
            httpClient = new HttpClient();
            Places = new ObservableCollection<Place>();
            SelectedShop = _sharedService.GetValue<ShopModel>("SelectedShop");
            if (SelectedShop != null)
            {
                IsVisible = true;
                SelectedPlaceDetails= SelectedShop.Location;
            }
            else
            {
                SelectedShop = new ShopModel();
                IsVisible = false;
            }
            // Initialize commands
            AddShopCommand = new AsyncRelayCommand(AddShopAsync);
            DeleteShopCommand = new AsyncRelayCommand(DeleteShopAsync);
        }
        private async Task AddShopAsync()
        {
            try
            {
                Common.BusyIndicator(true);
                if (SelectedShop == null || string.IsNullOrEmpty(SelectedPlaceDetails.Address))
                {
                    await Application.Current.MainPage.DisplayAlert("", "Select a location first", "Ok");
                    return;
                }
                SelectedShop.Location = SelectedPlaceDetails;
                if (SelectedShop.ShopId != Guid.Empty)
                {
                    await _shopService.UpdateShopAsync(SelectedShop);
                }
                else
                    await _shopService.AddShopAsync(SelectedShop);

                Common.BusyIndicator(false);
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }

        [RelayCommand]
        private async Task EditShop()
        {
            _sharedService.Add<ShopModel>("SelectedShop", SelectedShop);
            await Shell.Current.GoToAsync(nameof(AddShop));
        }
        [RelayCommand]
        private async Task ViewOnMap()
        {
            _sharedService.Add<ShopModel>("SelectedShop", SelectedShop);
            await Shell.Current.GoToAsync(nameof(ShopMapView));
        }
        private async Task DeleteShopAsync()
        {
            await MopupService.Instance.PushAsync(new DeleteOnlyPopup(SelectedShop.ShopId));
            MessagingCenter.Subscribe<string>(this, SelectedShop.ShopId.ToString(), async (sender) =>
            {
                if (sender == "Delete")
                {
                    await _shopService.DeleteShopAsync(SelectedShop.ShopId);
                    await Shell.Current.GoToAsync("..");
                }
                MessagingCenter.Unsubscribe<string>(this, "Delete");
            });
        }
        private async Task SearchPlacesAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    Places.Clear();
                    return;
                }
                var url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={SearchText}&key={apiHelper.googleMapApiKey}";
                var response = await httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<PlaceSearchResponse>(response);
                Places = new ObservableCollection<Place>(result.Predictions);
            }
            catch (Exception ex)
            { 
            
            }
        }
        [RelayCommand]
        public async Task SelectLocation(Place place)
        {
            try
            { 
                var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={place.PlaceId}&key={apiHelper.googleMapApiKey}";
                var response = await httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<PlaceDetailsResponse>(response);
                SelectedPlaceDetails = result.Result;
                Places.Clear();
                SearchText = string.Empty;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
