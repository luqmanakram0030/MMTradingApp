namespace MMAdmin;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        RegisterRoutes();
        CheckLoginAndSetCurrentItem();
    }
    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(AdminRegisterView), typeof(AdminRegisterView));
        Routing.RegisterRoute(nameof(AdminLoginView), typeof(AdminLoginView));
        Routing.RegisterRoute(nameof(EmployeeView), typeof(EmployeeView));
        Routing.RegisterRoute(nameof(AddEmployee), typeof(AddEmployee));
        Routing.RegisterRoute(nameof(AdminForgetPasswordView), typeof(AdminForgetPasswordView));
    }
    private void CheckLoginAndSetCurrentItem()
    {
        bool isloggedin = Preferences.Get("isloggedin", false);
        CurrentItem = isloggedin ? DashboardView : Login;
    }
   
}

