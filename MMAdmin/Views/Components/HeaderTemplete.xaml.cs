
using System.Net.Http;
using System.Windows.Input;

namespace MMAdmin.Views.Components;

public partial class HeaderTemplete : ContentView
{
    public static BindableProperty CommandProperty = BindableProperty.Create(
          nameof(Command),
          typeof(ICommand),
          typeof(HeaderTemplete),
          defaultBindingMode: BindingMode.TwoWay);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public HeaderTemplete()
	{
		InitializeComponent();
        LoadDefaults();
    }
    private void LoadDefaults()
    {
       UserName.Text =  Preferences.Default.Get("UserName", "");
        // UserLocation.Text = Preferences.Get("userloctaion", "");
        // ProfileImage.ImageSource = Preferences.Get("UserAvatar", null);
        ProfileImage.Text= Preferences.Get("Initials", "");
    }

}
