using MMAdmin.ViewModels.ShopManagement;

namespace MMAdmin.Views.ShopManagement;

public partial class AddShop : ContentPage
{
    AddShopViewModel addshopViewModel;
    public AddShop(AddShopViewModel _addshopViewModel)
	{
		InitializeComponent();
        addshopViewModel = _addshopViewModel;
        BindingContext = addshopViewModel;
    }
}