namespace MMAdmin.Views;

public partial class EmployeeView : ContentPage
{
	
    EmployeesViewModel employeeViewModel;
    public EmployeeView(EmployeesViewModel _employeeViewModel)
    {
        try
        {
            InitializeComponent();
            this.employeeViewModel = _employeeViewModel;
            BindingContext = employeeViewModel;
        }
        catch (Exception ex)
        {
            _ = ex.Message;
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await employeeViewModel.LoadEmployeesAsync();
    }

}
