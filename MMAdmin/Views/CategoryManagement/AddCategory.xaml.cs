using MMAdmin.ViewModels.CategoryManagement;

namespace MMAdmin.Views.CategoryManagement;

public partial class AddCategory : ContentPage
{
    AddCategoryViewModel addCategoryViewModel;
    public AddCategory(AddCategoryViewModel _addCategoryViewModel)
	{
		InitializeComponent();
		addCategoryViewModel = _addCategoryViewModel;
		BindingContext = addCategoryViewModel;
	}
}