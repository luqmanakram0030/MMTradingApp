using MMAdmin.ViewModels.ProductManagement;

namespace MMAdmin.Views.ProductManagement;

public partial class AddProductView : ContentPage
{
    AddProductViewModel addProductViewModel;
    public AddProductView(AddProductViewModel _addProductViewModel)
	{
		InitializeComponent();
		addProductViewModel = _addProductViewModel;
		BindingContext = addProductViewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await addProductViewModel.LoadCategoryAsync();
    }
}