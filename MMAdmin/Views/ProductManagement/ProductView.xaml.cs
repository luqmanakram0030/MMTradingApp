using MMAdmin.ViewModels.ProductManagement;

namespace MMAdmin.Views;

public partial class ProductView : ContentPage
{
    ProductViewModel productViewModel;
    public ProductView(ProductViewModel _productViewModel)
	{
		InitializeComponent();
        productViewModel = _productViewModel;
        BindingContext = productViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await productViewModel.LoadProductAsync();
    }

}
