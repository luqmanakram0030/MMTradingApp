using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMAdmin.PracticeProjects.Model;
using MMAdmin.PracticeProjects.Services;
using MMAdmin.PracticeProjects.Views;
using MMAdmin.PracticeProjects.ViewModels;
using System.IO;
using System.Windows.Input;
using SQLite;

namespace MMAdmin.PracticeProjects.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly databaseHelper _database;

        public ObservableCollection<ProductModel> Products { get; set; }
        public ICommand LoadProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public MainViewModel()
        {
            _database = App.Database;
            Products = new ObservableCollection<ProductModel>();
            LoadProductsCommand = new Command(async () => await LoadProducts());
            AddProductCommand = new Command(async () => await AddProduct());
            EditProductCommand = new Command<ProductModel>(async (product) => await EditProduct(product));
            DeleteProductCommand = new Command<ProductModel>(async (product) => await DeleteProduct(product));
           
            MessagingCenter.Subscribe<AddorEditViewModel>(this, "RefreshProducts", async (sender) =>
            {
                await LoadProducts(); // 🛠 Load Only Once
            });

            Task.Run(async () => await LoadProducts());
        }

        public async Task LoadProducts()
        {
            var products = await _database.GetProducts();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async Task AddProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddorEdit());
        }
        private async Task EditProduct(ProductModel product)
        {
            if (product != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddorEdit(product));
            }
        }

        private async Task DeleteProduct(ProductModel product)
        {
            if (product != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Do you want to delete this product?", "Yes", "No");
                if (confirm)
                {
                    await _database.DeleteProduct(product);
                    Products.Remove(product);
                }
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
    }
}
