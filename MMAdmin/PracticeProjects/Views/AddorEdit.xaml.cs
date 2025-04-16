using MMAdmin.PracticeProjects.Model;
using MMAdmin.PracticeProjects.ViewModels;
using MMAdmin.PracticeProjects.Services;

namespace MMAdmin.PracticeProjects.Views;

public partial class AddorEdit : ContentPage
{
	public AddorEdit(ProductModel product = null)
	{
		InitializeComponent();
        BindingContext = new AddorEditViewModel(product);
    }
}