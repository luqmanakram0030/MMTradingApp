using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using MMAdmin.PracticeProjects.Model;
using MMAdmin.PracticeProjects.Services;
using MMAdmin.PracticeProjects.ViewModels;
using System.Windows.Input;


namespace MMAdmin.PracticeProjects.ViewModels
{
    public class AddorEditViewModel : BaseViewModel
    {
        private readonly databaseHelper _database;
        
        public ProductModel Product {  get; set; }
        public ICommand SaveCommand { get;}
        public ICommand AddImageCommand { get; }


        public AddorEditViewModel(ProductModel product = null)
        {
            _database = App.Database;
            Product = product ?? new ProductModel();
            SaveCommand = new Command(async () => await SaveProduct());
            AddImageCommand = new Command(async () => await AddImage());
        }
      

        private async Task AddImage()
        {
            var action = await Application.Current.MainPage.DisplayActionSheet("Select Image Source", "Cancel", null, "Camera", "Gallery");
            FileResult result = null;

            if (action == "Camera")
                result = await MediaPicker.CapturePhotoAsync();
            else if (action == "Gallery")
                result = await MediaPicker.PickPhotoAsync();

            if (result != null)
            {
                Product.Images = result.FullPath;
                OnPropertyChanged(nameof(Product));
            }
        }


        private async Task SaveProduct()
        {
            if (!string.IsNullOrWhiteSpace(Product.Name) && !string.IsNullOrWhiteSpace
                (Product.Description) && !string.IsNullOrWhiteSpace
                (Product.Category) && Product.Price > 0 && Product.StockQty > 0)
            {
                if (Product.Id == 0)
                {
                    await _database.AddProduct(Product);
                }
                else
                {
                    await _database.UpdateProduct(Product);
                }
                MessagingCenter.Send(this, "RefreshProducts");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter all details", "OK");
            }
        }
    }
}
