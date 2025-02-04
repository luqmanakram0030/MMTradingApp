using MMAdmin.Abstract;
using MMAdmin.Views.Popup;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMAdmin.Views.CategoryManagement;

namespace MMAdmin.ViewModels.CategoryManagement
{
    public partial class AddCategoryViewModel: ObservableObject
    {
        public IAsyncRelayCommand AddCategoryCommand { get; }
        public IAsyncRelayCommand DeleteCategoryCommand { get; }

        private readonly ISharedService _sharedService;

        [ObservableProperty]
        private bool isVisible;

        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        private Category selectedCategory;
        public AddCategoryViewModel(ISharedService sharedService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            this._sharedService = sharedService;

            SelectedCategory = _sharedService.GetValue<Category>("SelectedCategory");
            if (SelectedCategory != null)
                IsVisible = true;
            else
            {
                SelectedCategory = new Category();
                IsVisible = false;
            }
            // Initialize commands
            AddCategoryCommand = new AsyncRelayCommand(AddCategoryAsync);
            DeleteCategoryCommand = new AsyncRelayCommand(DeleteCategoryAsync);
        }
        private async Task AddCategoryAsync()
        {
            try
            {
                Common.BusyIndicator(true);
                if (SelectedCategory == null)
                    return;
                if (SelectedCategory.Id != Guid.Empty)
                {
                    await _categoryService.UpdateCategoryAsync(SelectedCategory);
                    await Shell.Current.GoToAsync("..");
                }
                else
                    await _categoryService.AddCategoryAsync(SelectedCategory);
                await Shell.Current.GoToAsync("..");
                Common.BusyIndicator(false);
            }
            catch (Exception ex)
            {
                Common.BusyIndicator(false);
            }
            
        }
        [RelayCommand]
        private async Task GoBackAsync(Object obj)
        {
            try
            {
                var page = obj as AddCategory;
                var btnAddEmployee = page.FindByName("btngoback");
                

                await Common.ControlBounceEffect(btnAddEmployee);
                await Shell.Current.GoToAsync("..");
                // await Shell.Current.GoToAsync("..");
                
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }
        private async Task DeleteCategoryAsync()
        {
            try
            {
                await MopupService.Instance.PushAsync(new DeleteOnlyPopup(SelectedCategory.Id));
                MessagingCenter.Subscribe<string>(this, SelectedCategory.Id.ToString(), async (sender) =>
                {
                    if (sender == "Delete")
                    {
                        await _categoryService.DeleteCategoryAsync(SelectedCategory.Id);
                        await Shell.Current.GoToAsync("..");
                    }
                    MessagingCenter.Unsubscribe<string>(this, "Delete");
                });
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}
