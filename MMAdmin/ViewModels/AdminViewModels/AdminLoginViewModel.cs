using System;
using Android.Widget;
using Firebase.Auth;
using Newtonsoft.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MMAdmin.Domain.Services.Interface;
using CommunityToolkit.Mvvm.Input;
using MMAdmin.Views.AdminUserMangement;

namespace MMAdmin.ViewModels.AdminViewModels
{
	public partial class AdminLoginViewModel: ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password;
        
        private readonly FirebaseWebApi _webApi;

        private readonly IAdminUser _userService;
       
       
        #endregion

        #region Constructor
        public AdminLoginViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;

            _webApi = new FirebaseWebApi();
            _userService = DependencyService.Resolve<IAdminUser>();

           

        }
        #endregion

        #region Methods
        [RelayCommand]
        public async Task Login()
        {
            if (!await CheckInternetConnectionAsync())
            {
                await ShowErrorAsync("Connect your device to the internet");
                return;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await ShowErrorAsync("Please enter your email.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await ShowErrorAsync("Please enter your password.");
                return;
            }

            try
            {
                
                var response = await _userService.LoginAsync(Email.Trim().ToLower());

                if (response != null)
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApi.WebAPIKey));
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email.Trim().ToLower(), Password);

                    var content = await auth.GetFreshAuthAsync();

                    var serializedcontnet = JsonConvert.SerializeObject(content);

                    Preferences.Set("Email", Email.Trim().ToLower());
                    Preferences.Set("Islogined", "true");
                    Preferences.Set("Name", response.FullName.Trim());
                   

                    
                        Application.Current.MainPage = new AppShell();
                   
                }
                else
                {
                   
                    await ShowErrorAsync("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
               
                await ShowErrorAsync("Invalid email or password");
            }
        }
        [RelayCommand]
        public async Task Signup() => await Application.Current.MainPage.Navigation.PushAsync(new AdminRegisterView());
        [RelayCommand]
        public async Task NavigateForgotPassword() => await Application.Current.MainPage.Navigation.PushAsync(new AdminForgetPasswordView());

        private async Task<bool> CheckInternetConnectionAsync()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }

        private async Task ShowErrorAsync(string message)
        {
            
            await ToastService.ShowToastAsync(message);
        }
        #endregion
      
	}
}

