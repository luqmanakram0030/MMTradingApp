namespace MMAdmin.Views.AdminUserMangement;

public partial class AdminRegisterView : ContentPage
{
    
    public AdminRegisterView(AdminRegisterViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
       
    }
    
}
