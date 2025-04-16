namespace MMAdmin;

using MMAdmin.PracticeProjects.Services;
using MMAdmin.Views;
using MMAdmin.PracticeProjects.Views;
using System.IO;
using Microsoft.Maui.Graphics;

public partial class App : Application
{
    public static databaseHelper Database;
	public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgwODkxNUAzMjMzMmUzMDJlMzBBMlNESW9YanNjejYxQldPbVFBVHZ6dUUvZWtzMjNyekZWTjdLWVlJK2JzPQ ==");
        InitializeComponent();
        //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MMAdmin.db3");
        //Database = new databaseHelper(dbPath);
        //MainPage = new NavigationPage(new PracticeProjects.Views.MainPage())
        //{
        //    BarBackgroundColor = Color.FromArgb("#6200EE"),
        //    BarTextColor = Colors.White
        //};
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}

