namespace MMAdmin.Views.AdminUserMangement;

public partial class AdminLoginView : ContentPage
{
    public AdminLoginViewModel adminLoginViewModel;
	public AdminLoginView(AdminLoginViewModel _adminLoginViewModel)
	{
		InitializeComponent();
        BindingContext = adminLoginViewModel= _adminLoginViewModel;

    }
     
}
