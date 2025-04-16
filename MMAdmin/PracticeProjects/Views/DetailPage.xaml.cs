
using MMAdmin.PracticeProjects.Model;
using MMAdmin.PracticeProjects.ViewModels;
using MMAdmin.PracticeProjects.Services;
using System.Collections.ObjectModel;

namespace MMAdmin.PracticeProjects.Views;

public partial class DetailPage : ContentPage
{
    public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();

    public ProductModel Product { get; set; }
    public MainViewModel ViewModel { get; set; }

    public DetailPage(ProductModel selectedProduct, MainViewModel mainViewModel)
    {
        InitializeComponent();
        Product = selectedProduct;
        ViewModel = mainViewModel;

        // Add the main product image to the carousel initially
        if (!string.IsNullOrEmpty(Product.Images))
            Images.Add(Product.Images);

        this.BindingContext = this;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.PickPhotoAsync();

        if (result != null)
        {
            // Add selected image to carousel
            Images.Add(result.FullPath);
        }
    }
}
