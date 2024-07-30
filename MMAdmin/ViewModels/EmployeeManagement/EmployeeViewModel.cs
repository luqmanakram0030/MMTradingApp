using MMAdmin.Abstract;
using MMAdmin.Views.Popup;
using Mopups.Services;
using System.Windows.Input;

namespace MMAdmin.ViewModels.EmployeeManagement;
public partial class EmployeesViewModel : ObservableObject
{
    #region Fields
    private ObservableCollection<EmployeeModel> _FilteredList;
    private ObservableCollection<EmployeeModel> _UnfilteredList;
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
    public IAsyncRelayCommand LoadEmployeesCommand { get; }
    private readonly ISharedService _sharedService;
    private readonly IEmployeeService _employeeService;

    [ObservableProperty]
    private ObservableCollection<EmployeeModel> employees;

    public EmployeesViewModel(ISharedService sharedService, IEmployeeService employeeService)
    {
        _employeeService = employeeService;
        this._sharedService = sharedService;
        Employees = new ObservableCollection<EmployeeModel>();
        LoadEmployeesCommand = new AsyncRelayCommand(LoadEmployeesAsync);
        SearchCommand = new Command(async () => await PerformSearch());
    }
    #region Methods
    public async Task LoadEmployeesAsync()
    {
        try
        {
            Common.BusyIndicator(true);
            Employees.Clear();
            var employees = await _employeeService.GetAllEmployeesAsync();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
            _UnfilteredList = Employees;
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
            Employees = _UnfilteredList;
        }
        else
        {
            string searchTextLower = _searchText.ToLower();
            _FilteredList = new ObservableCollection<EmployeeModel>(
                _UnfilteredList.Where(i =>
                    i.FullName.ToLower().Contains(searchTextLower) ||
                    i.Email.ToLower().Contains(searchTextLower)));

            Employees = _FilteredList;
        }
    }


    [RelayCommand]
    private async Task NavigateToAddEmployee(EmployeeModel employee)
    {
        _sharedService.Add<EmployeeModel>("SelectedEmployee", employee);
        await Shell.Current.GoToAsync(nameof(AddEmployee));
    }
    #endregion
}


