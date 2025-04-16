using MMAdmin.PracticeProjects.Model;
using MMAdmin.PracticeProjects.ViewModels;
using MMAdmin.PracticeProjects.Services;
using System.Threading.Tasks;


namespace MMAdmin.PracticeProjects.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel viewModel)
        {
            await viewModel.LoadProducts();
        }
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null && e.Item is ProductModel SelectedProduct)
        {
            if (BindingContext is MainViewModel vm)
            {
                await Navigation.PushAsync(new DetailPage(SelectedProduct, vm));
            }
        }
    }
}