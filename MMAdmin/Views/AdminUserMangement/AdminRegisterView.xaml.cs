namespace MMAdmin.Views.AdminUserMangement;

public partial class AdminRegisterView : ContentPage
{
    private readonly Entry _entryTapped;
    public AdminRegisterView()
	{
		InitializeComponent();
        _entryTapped = new Entry();
    }
    private async void Signup_Tapped(object sender, EventArgs e)
    {
        //  await Navigation.PushAsync(new RegisterPage());
    }

    private async void ForgotPassword_Tapped(object sender, EventArgs e)
    {
        // await Navigation.PushAsync(new ForgetPasswordPage());
    }

    private void Login_Clicked(object sender, EventArgs e)
    {
        // Application.Current.MainPage = new AppShell();
    }
}
