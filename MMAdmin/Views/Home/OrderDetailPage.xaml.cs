namespace MMAdmin.Views.Home;
using MMAdmin.Domain.Models;

public partial class OrderDetailPage : ContentPage
{
	public OrderDetailPage(OrdersModels selectedOrder)
	{
		InitializeComponent();
		BindingContext = selectedOrder;
	}
}