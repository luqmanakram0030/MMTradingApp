using MMAdmin.Abstract;
using MMAdmin.Views.CategoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMAdmin.ViewModels.CategoryManagement
{
    public partial class CategoryViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<Category> _FilteredList;
        private ObservableCollection<Category> _UnfilteredList;
        private string _searchText;
        #endregion
        #region Properties
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.Execute(null);
            }
        }
        #endregion
        public ICommand SearchCommand { get; set; }
        public IAsyncRelayCommand LoadCategoryCommand { get; }
        private readonly ISharedService _sharedService;
        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        private ObservableCollection<Category> categories;
        public CategoryViewModel(ISharedService sharedService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            this._sharedService = sharedService;
            Categories = new ObservableCollection<Category>();
            LoadCategoryCommand = new AsyncRelayCommand(LoadCategoryAsync);
            SearchCommand = new Command(async () => await PerformSearch());
        }
        #region Methods
        public async Task LoadCategoryAsync()
        {
            try
            {
                Common.BusyIndicator(true);
                Categories.Clear();
                var categories = await _categoryService.GetAllCategoriesAsync();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
                _UnfilteredList = Categories;
                Common.BusyIndicator(false);
            }
            catch (Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }

        public async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(this._searchText))
            {
                Categories = _UnfilteredList;
            }
            else
            {
                string searchTextLower = _searchText.ToLower();
                _FilteredList = new ObservableCollection<Category>(
                    _UnfilteredList.Where(i =>
                        i.Name.ToLower().Contains(searchTextLower)));

                Categories = _FilteredList;
            }
        }


        [RelayCommand]
        private async Task NavigateToAddCategory(Category category)
        {
            _sharedService.Add<Category>("SelectedCategory", category);
            await Shell.Current.GoToAsync(nameof(AddCategory));
        }
        #endregion
    }
}
