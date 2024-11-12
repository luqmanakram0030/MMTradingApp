using MMAdmin.ViewModels.ProductManagement;

namespace MMAdmin.Views.ProductManagement;

public partial class AddProductView : ContentPage
{
    AddProductViewModel addProductViewModel;
    public AddProductView(AddProductViewModel _addProductViewModel)
	{
		try
		{
			InitializeComponent();
			addProductViewModel = _addProductViewModel;
			BindingContext = addProductViewModel;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			//throw;
		}
		
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
      // await addProductViewModel.LoadCategoryAsync();
    }
}