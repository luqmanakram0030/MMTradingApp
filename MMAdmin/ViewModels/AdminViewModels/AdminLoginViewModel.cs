


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

        private readonly IAdminUser userService;
       
       
        #endregion

        #region Constructor
        public AdminLoginViewModel(IAdminUser _userService)
        {
            Email = string.Empty;
            Password = string.Empty;
            _webApi = new FirebaseWebApi();
            this.userService = _userService;

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
                
                var response = await userService.LoginAsync(Email.Trim().ToLower());

                if (response != null)
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApi.WebAPIKey));
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email.Trim().ToLower(), Password);
                    var content = await auth.GetFreshAuthAsync();

                    var serializedcontnet = JsonConvert.SerializeObject(content);

                    Preferences.Set("Email", Email.Trim().ToLower());
                    Preferences.Set("isloggedin", true);
                    
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
               
                await ShowErrorAsync("Invalid email or password("+ex.Message+")");
            }
        }
        [RelayCommand]
        public async Task Signup() => await Shell.Current.GoToAsync(nameof(AdminRegisterView));
        [RelayCommand]
        public async Task NavigateForgotPassword() => await Shell.Current.GoToAsync(nameof(AdminForgetPasswordView));

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

