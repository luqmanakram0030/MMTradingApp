using MMAdmin.ViewModels.ShopManagement;

namespace MMAdmin.Views.ShopManagement;

public partial class ShopMapView : ContentPage
{
	ShopMapViewModel shopMapViewModel;

    public ShopMapView(ShopMapViewModel _shopMapViewModel)
	{
		InitializeComponent();
		shopMapViewModel = _shopMapViewModel;
		BindingContext = shopMapViewModel;
        shopMapViewModel.GetUserCoordinatesCommand.Execute(map);
    }
}