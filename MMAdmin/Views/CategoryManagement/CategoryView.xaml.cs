using MMAdmin.ViewModels.CategoryManagement;

namespace MMAdmin.Views;

public partial class CategoryView : ContentPage
{
	CategoryViewModel categoryViewModel;
    public CategoryView(CategoryViewModel _categoryViewModel)
	{
		InitializeComponent();
        categoryViewModel = _categoryViewModel;
        BindingContext = categoryViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await categoryViewModel.LoadCategoryAsync();
    }
}