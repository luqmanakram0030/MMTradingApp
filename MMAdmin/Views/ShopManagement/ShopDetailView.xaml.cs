using MMAdmin.ViewModels.ShopManagement;

namespace MMAdmin.Views.ShopManagement;

public partial class ShopDetailView : ContentPage
{
	AddShopViewModel addShopViewModel;
    public ShopDetailView(AddShopViewModel _addShopViewModel)
	{
		InitializeComponent();
        addShopViewModel = _addShopViewModel;
        BindingContext = addShopViewModel;
    }
}