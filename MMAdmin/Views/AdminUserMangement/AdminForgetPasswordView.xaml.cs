namespace MMAdmin.Views.AdminUserMangement;

public partial class AdminForgetPasswordView : ContentPage
{
	public AdminForgetPasswordView()
	{
		InitializeComponent();
	}
    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
