using MMAdmin.Views.CategoryManagement;
using MMAdmin.Views.ProductManagement;
using MMAdmin.Views.ShopManagement;

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
        Routing.RegisterRoute(nameof(AddEmployee), typeof(AddEmployee));
        Routing.RegisterRoute(nameof(ShopView), typeof(ShopView));
        Routing.RegisterRoute(nameof(AddCategory), typeof(AddCategory));
        Routing.RegisterRoute(nameof(CategoryView), typeof(CategoryView));
        Routing.RegisterRoute(nameof(AddProductView), typeof(AddProductView));
        Routing.RegisterRoute(nameof(ProductView), typeof(ProductView));
        Routing.RegisterRoute(nameof(ShopDetailView), typeof(ShopDetailView));
        Routing.RegisterRoute(nameof(AddShop), typeof(AddShop));
        Routing.RegisterRoute(nameof(ShopMapView), typeof(ShopMapView));
        Routing.RegisterRoute(nameof(AdminForgetPasswordView), typeof(AdminForgetPasswordView));
    }
    private void CheckLoginAndSetCurrentItem()
    {
        bool isloggedin = Preferences.Get("isloggedin", false);
        CurrentItem = isloggedin ? DashboardView : Login;
    }
   
}

