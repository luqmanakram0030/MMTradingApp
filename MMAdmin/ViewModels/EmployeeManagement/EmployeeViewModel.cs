

namespace MMAdmin.ViewModels.EmployeeManagement;
public partial class EmployeesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<EmployeeModel> employeeList;

    [ObservableProperty]
    private ObservableCollection<string> filterDataList;

    [ObservableProperty]
    private ObservableCollection<EmployeeModel> unfilteredEmployeesList;

    public IAsyncRelayCommand LoadEmployeesCommand { get; }
    public IAsyncRelayCommand AddEmployeeCommand { get; }
    public IAsyncRelayCommand UpdateEmployeeCommand { get; }
    public IAsyncRelayCommand DeleteEmployeeCommand { get; }


    [ObservableProperty]
    private bool filterIsVisible;

    [ObservableProperty]
    private string searchText;




    private readonly IEmployeeService _employeeService;

    [ObservableProperty]
    private ObservableCollection<EmployeeModel> employees;

    [ObservableProperty]
    private EmployeeModel selectedEmployee;

    public EmployeesViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
        Employees = new ObservableCollection<EmployeeModel>();

        // Initialize commands
        LoadEmployeesCommand = new AsyncRelayCommand(LoadEmployeesAsync);
        AddEmployeeCommand = new AsyncRelayCommand(AddEmployeeAsync);
        UpdateEmployeeCommand = new AsyncRelayCommand(UpdateEmployeeAsync);
        DeleteEmployeeCommand = new AsyncRelayCommand(DeleteEmployeeAsync);
    }




    #region Methods
    public async Task LoadEmployeesAsync()
    {
        Employees.Clear();
        var employees = await _employeeService.GetAllEmployeesAsync();
        foreach (var employee in employees)
        {
            Employees.Add(employee);
        }
    }

    private async Task AddEmployeeAsync()
    {
        if (SelectedEmployee == null)
            return;

        await _employeeService.AddEmployeeAsync(SelectedEmployee);
        await LoadEmployeesAsync();
    }

    private async Task UpdateEmployeeAsync()
    {
        if (SelectedEmployee == null)
            return;

        await _employeeService.UpdateEmployeeAsync(SelectedEmployee);
        await LoadEmployeesAsync();
    }

    private async Task DeleteEmployeeAsync()
    {
        if (SelectedEmployee == null)
            return;

        await _employeeService.DeleteEmployeeAsync(SelectedEmployee.UserId);
        await LoadEmployeesAsync();
    }
    public async Task GetAllData()
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            Common.DisplayErrorMessage("GetLeadsData: " + ex.Message);
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        try
        {
            FilterLeads();
        }
        catch (Exception ex)
        {
            Common.DisplayErrorMessage("SearchLeadsData: " + ex.Message);
        }
    }

    [RelayCommand]
    private void RemoveFilterData(string filterData)
    {
        if (FilterDataList.Contains(filterData))
        {
            FilterDataList.Remove(filterData);
            FilterLeads();
        }
    }

    [RelayCommand]
    public void ToggleFilterVisibility()
    {
        try
        {
            FilterIsVisible = !FilterIsVisible;
        }
        catch (Exception ex)
        {
            Common.DisplayErrorMessage("ToggleFilterVisibility: " + ex.Message);
        }
    }

    [RelayCommand]
    public async Task ApplyFilter(object obj)
    {
        await Common.ControlBounceEffect(obj);
        try
        {
           

            FilterLeads();
        }
        catch (Exception ex)
        {
            Common.DisplayErrorMessage("ApplyFilter: " + ex.Message);
        }
    }

    private void FilterLeads()
    {
      
    }
    [RelayCommand]
    private async Task NavigateToDetail()
    {
        
             
    }
    [RelayCommand]
    private async Task NavigateToAddNewLead(Object obj)
    {


        await Shell.Current.GoToAsync(nameof(AddEmployee));

    }
    #endregion
}


