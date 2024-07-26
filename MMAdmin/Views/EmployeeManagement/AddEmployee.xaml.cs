namespace MMAdmin.Views.EmployeeManagement;

public partial class AddEmployee : ContentPage
{
    EmployeesViewModel employeeViewModel;
    public AddEmployee(EmployeesViewModel _employeeViewModel)
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
   
   

}
