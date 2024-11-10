using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMAdmin.ViewModels.ProductManagement;

namespace MMAdmin.Views.ProductManagement;

public partial class ProductDetailView : ContentPage
{
    ProductDetailViewModel ProductDetailViewModel;
    public ProductDetailView(ProductDetailViewModel _productViewModel)
    {
        InitializeComponent();
        ProductDetailViewModel = _productViewModel;
        BindingContext = ProductDetailViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ProductDetailViewModel.LoadCategoryAsync();
    }
}