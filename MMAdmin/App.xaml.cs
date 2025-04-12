namespace MMAdmin;

public partial class App : Application
{
	public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgwODkxNUAzMjMzMmUzMDJlMzBBMlNESW9YanNjejYxQldPbVFBVHZ6dUUvZWtzMjNyekZWTjdLWVlJK2JzPQ ==");
        InitializeComponent();
    //MainPage = new AppShell();
    //MainPage = new NavigationPage(new AdminLoginView());
    }
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}

