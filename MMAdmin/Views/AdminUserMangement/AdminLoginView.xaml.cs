﻿namespace MMAdmin.Views.AdminUserMangement;

public partial class AdminLoginView : ContentPage
{
	public AdminLoginView()
	{
		InitializeComponent();
	}
    private async void Signup_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminRegisterView());
    }

    private async void ForgotPassword_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminForgetPasswordView());
    }

    private void Login_Clicked(object sender, EventArgs e)
    {
       // Application.Current.MainPage = new AppShell();
    }

    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await ToastService.ShowToastAsync("Hello, this is a toast message!");
    }
}
