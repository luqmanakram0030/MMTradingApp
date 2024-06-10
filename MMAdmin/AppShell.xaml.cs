namespace MMAdmin;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        RegisterRoutes();
        CheckLoginAndSetCurrentItem();
    }
    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(AdminRegisterView), typeof(AdminRegisterView));
        Routing.RegisterRoute(nameof(AdminLoginView), typeof(AdminLoginView));
        Routing.RegisterRoute(nameof(EmployeeView), typeof(EmployeeView));
        Routing.RegisterRoute(nameof(AdminForgetPasswordView), typeof(AdminForgetPasswordView));
    }
    private void CheckLoginAndSetCurrentItem()
    {
        bool isloggedin = Preferences.Get("isloggedin", false);
        CurrentItem = isloggedin ? Employee : Login;
    }
    protected override async void OnNavigated(ShellNavigatedEventArgs args)
    {
        if (args.Source == ShellNavigationSource.ShellSectionChanged)
        {
            foreach (var tab in MyTabbar.Items)
            {
                await tab.Navigation.PopToRootAsync();
            }

            for (int i = 0; i < MyTabbar.Items.Count; i++)
            {
                bool isCurrentPage = MyTabbar.CurrentItem == MyTabbar.Items[i];
                var img = (FontImageSource)MyTabbar.Items[i].Icon;

                // Get the appropriate glyph based on whether the tab is selected or not
                string glyph = GetGlyphForIndex(i, isCurrentPage);

                // Update the icon for the tab
                MyTabbar.Items[i].Icon = new FontImageSource { Glyph = glyph, FontFamily = img.FontFamily, Size = 20, Color = isCurrentPage ? Color.FromHex("#1ABB9C") : Color.FromHex("#000000") };
            }
        }
        base.OnNavigated(args);
    }

    // Method to get the selected glyph based on the tab index (Replace with your logic)

    private string GetGlyphForIndex(int index, bool isSelected)
    {

        switch (index)
        {
            case 0:
                return isSelected ? "\U000f056e" : "\U000f0a1d"; // Unicode for the selected and unselected icons
            case 1:
                return isSelected ? "\U000f0d45" : "\U000f0365"; // Unicode for the selected and unselected icons
            case 2:
                return isSelected ? "\U000f009a" : "\U000f009c"; // Unicode for the selected and unselected icons
            case 3:
                return isSelected ? "\U000f0004" : "\U000f0013"; // Unicode for the selected and unselected icons
            case 4:
                return isSelected ? "\U000f0e17" : "\U000f0e18"; // Unicode for the selected and unselected icons

            default:
                return "\uf056e"; // Default icon
        }
    }
}

