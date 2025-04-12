using System.Threading.Tasks;
using System.Windows.Input;
using MMAdmin.Views;
using MMAdmin.Views.Home;

namespace MMAdmin.Views.Components;

public partial class ActivityButtonsTemplate : ContentView
{
    public static readonly BindableProperty OrderCountProperty = BindableProperty.Create(nameof(OrderCount), typeof(string), typeof(ActivityButtonsTemplate), "0");
    public static readonly BindableProperty OrderStatusProperty = BindableProperty.Create(nameof(OrderStatus), typeof(string), typeof(ActivityButtonsTemplate), "No Process");
    public static readonly BindableProperty CustomBgProperty = BindableProperty.Create(nameof(CustomBg), typeof(Color), typeof(ActivityButtonsTemplate));

    public string OrderCount
    {
        get => (string)GetValue(OrderCountProperty);
        set => SetValue(OrderCountProperty, value);
    }

    public string OrderStatus
    {
        get => (string)GetValue(OrderStatusProperty);
        set => SetValue(OrderStatusProperty, value);
    }

    public Color CustomBg
    {
        get => (Color)GetValue(CustomBgProperty);
        set => SetValue(CustomBgProperty, value);
    }
    public ActivityButtonsTemplate()
    {
        InitializeComponent();
        BindingContext = this;
        
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        string orderCount = OrderCount;
        string orderStatus = OrderStatus;
        await Navigation.PushAsync(new OrdersView(orderCount, orderStatus));
    }
}
