using System;
using System.Windows.Input;

namespace MMAdmin.ViewModels.AdminViewModels
{
	public partial class AdminRegisterViewModel : ObservableObject
    {

        #region Properties

        [ObservableProperty]
        public string confirmPassword;
        

        private readonly FirebaseWebApi _webApi;

        private readonly IAdminUser userService;

        [ObservableProperty]
        public AdminUserModel user;
       
       

        #endregion

        #region Constructor
        public AdminRegisterViewModel(IAdminUser _userService)
        {
            ConfirmPassword = string.Empty;
            this.userService = _userService;
            User = new AdminUserModel();
            
            _webApi = new FirebaseWebApi();
           

           
        }
        #endregion

        #region Methods
        [RelayCommand]
        public async Task Signup()
        {
            if (!await CheckInternetConnectionAsync())
            {
                await ShowErrorAsync("Connect your device to internet.");
                return;
            }

            if (string.IsNullOrEmpty(User.Email))
            {
                await ShowErrorAsync("Please enter your email.");
                return;
            }

            

            if (User.Password != ConfirmPassword)
            {
                await ShowErrorAsync("Match passwords please.");
                return;
            }

            try
            {
                User.Email = User.Email.ToLower().Trim();
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApi.WebAPIKey));

               

                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(User.Email.Trim().ToLower(), User.Password);

                await userService.RegisterAsync(User);

                var content = await auth.GetFreshAuthAsync();

                var serializedcontnet = JsonConvert.SerializeObject(content);

                Preferences.Set("Email", User.Email.Trim().ToLower());

                Preferences.Set("Name", User.FullName.Trim());

               

                await ShowErrorAsync("Registered");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (FirebaseAuthException ex)
            {
                
                var message = $"Firebase authentication error: {ex.InnerException?.Message ?? ex.Message}";
                await ShowErrorAsync(message);
            }
            catch (Exception ex)
            {
                
                await ShowErrorAsync("Account already exists");
            }
        }
        [RelayCommand]
        public async Task Login() => await Shell.Current.GoToAsync(nameof(AdminLoginView));



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

