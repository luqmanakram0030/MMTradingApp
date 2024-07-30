
using Microsoft.Maui.Controls.Maps;
using MMAdmin.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace MMAdmin.ViewModels.ShopManagement
{
    public partial class ShopMapViewModel:ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private ObservableCollection<Pin> pins;

        private readonly IGeolocationService _geolocationService;
        private readonly Map map;
        [ObservableProperty]
        private string instructions;
        [ObservableProperty]
        private string distance;
        [ObservableProperty]
        private string duration;
        private readonly HttpClient httpClient;
        private readonly FirebaseWebApi apiHelper;
        private readonly IShopService _shopService;
        private readonly ISharedService _sharedService;
        [ObservableProperty]
        private ShopModel selectedShop;
        #endregion

        #region Commands
        public ICommand GetUserCoordinatesCommand { get; private set; }
        #endregion

        public ShopMapViewModel(ISharedService sharedService, IShopService shopService, IGeolocationService geolocationService)
        {
            Pins = new ObservableCollection<Pin>();
            apiHelper = new FirebaseWebApi();
            _geolocationService = geolocationService;
            _shopService = shopService;
            this._sharedService = sharedService;
            httpClient = new HttpClient();
            SelectedShop = _sharedService.GetValue<ShopModel>("SelectedShop");
            GetUserCoordinatesCommand = new Command<Map>(async mapActivity => await GetCurrentUserCoordinates(mapActivity));
        }
        public async Task GetCurrentUserCoordinates(Map map)
        {
            try
            {
                var position = await _geolocationService.GetCurrentPositionAsync();
                if (position != null)
                {
                    _geolocationService.MoveMapToPosition(map, position, 2);
                    var shopLocation = new Microsoft.Maui.Devices.Sensors.Location(SelectedShop.Location.Geometry.Location.Latitude,SelectedShop.Location.Geometry.Location.Latitude);
                    await _geolocationService.DrawRouteAsync(map, position, shopLocation);

                    pins.Add(new Pin()
                    {
                        Location = new Microsoft.Maui.Devices.Sensors.Location(position.Latitude, position.Longitude),
                        Label = "",
                        Address = ""
                    });
                    pins.Add(new Pin()
                    {
                        Location = new Microsoft.Maui.Devices.Sensors.Location(shopLocation),
                        Label = SelectedShop.Name,
                        Address = SelectedShop.Location.Address
                    });
                }
                else
                {
                    ToastService.ShowToastAsync("No Location Found.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowToastAsync("An error occurred while retrieving your location.");
            }
        }
        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
