using MMAdmin.ViewModels.ShopManagement;

namespace MMAdmin.Views;

public partial class ShopView : ContentPage
{
    ShopViewModel shopViewModel;
    public ShopView(ShopViewModel _shopViewModel)
	{
		InitializeComponent();
        shopViewModel = _shopViewModel;
        BindingContext = shopViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await shopViewModel.LoadShopsAsync();
    }
}
