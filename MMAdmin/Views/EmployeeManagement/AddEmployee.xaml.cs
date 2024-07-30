namespace MMAdmin.Views.EmployeeManagement;

public partial class AddEmployee : ContentPage
{
    AddEmployeeViewModel addemployeeViewModel;
    public AddEmployee(AddEmployeeViewModel _addemployeeViewModel)
    {
        try
        {
            InitializeComponent();
            this.addemployeeViewModel = _addemployeeViewModel;
            BindingContext = addemployeeViewModel;

        }
        catch (Exception ex)
        {
            _ = ex.Message;
        }
    }
   
   

}
