
#if WINDOWS
global using Microsoft.UI;
global using Microsoft.UI.Windowing;
global using Windows.Graphics;
#endif


using Microsoft.Maui.Handlers;
using MMAdmin.Abstract;
using MMAdmin.ViewModels.CategoryManagement;
using MMAdmin.ViewModels.Home;
using MMAdmin.ViewModels.ProductManagement;
using MMAdmin.ViewModels.ShopManagement;
using MMAdmin.Views.CategoryManagement;
using MMAdmin.Views.Home;
using MMAdmin.Views.ProductManagement;
using MMAdmin.Views.schedulerManagement;
using MMAdmin.Views.ShopManagement;
using MMEmployee.Views.schedulerManagement;
using Mopups.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace MMAdmin;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<Entry, EntryHandler>();
				handlers.AddHandler<Editor, EditorHandler>();
				handlers.AddHandler<Picker, PickerHandler>();

				Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
				{
#if IOS
                    if (handler.PlatformView is UIKit.UITextField textField)
                    {
                        textField.BorderStyle = UIKit.UITextBorderStyle.None;
                    }
#endif
				});

				Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
				{
#if IOS
                    if (handler.PlatformView is UIKit.UITextView textView)
                    {
                        textView.Layer.BorderWidth = 0;
                    }
#endif
				});

				Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
				{
#if IOS
                    if (handler.PlatformView is UIKit.UITextField textField)
                    {
                        textField.BorderStyle = UIKit.UITextBorderStyle.None;
                    }
#endif
				});
			})
            .ConfigureMopups()
#if !WINDOWS
            .UseMauiMaps()
#endif
             .ConfigureMauiHandlers(h =>
             {
#if ANDROID || IOS
                 h.AddHandler<Shell, TabbarBadgeRenderer>();
#endif
             })
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("materialdesignicons-webfont.ttf", "FontIcons");
            });
        FormHandler.RemoveBorders();

        // Register services
        builder.Services.AddSingleton<IGeolocationService, GeolocationService>();
        builder.Services.AddSingleton<IShopService, ShopService>();
        builder.Services.AddSingleton<ISharedService, SharedService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IEmployeeService, EmployeeService>();
        builder.Services.AddTransient<IScheduleService, ScheduleService>();
        
        builder.Services.AddTransient<IAdminUser, AdminUserService>();
        builder.Services.AddTransient<EmployeesViewModel>();
        builder.Services.AddTransient<EmployeeView>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<OrdersView>();
        builder.Services.AddTransient<AddEmployeeViewModel>();
        builder.Services.AddTransient<AddEmployee>();
        builder.Services.AddTransient<ShopViewModel>();
        builder.Services.AddTransient<ShopView>();
        builder.Services.AddTransient<AddShopViewModel>();
        builder.Services.AddTransient<AddShop>();
        builder.Services.AddTransient<AdminLoginView>();
        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<AdminLoginViewModel>();
        builder.Services.AddTransient<AdminRegisterViewModel>();
        builder.Services.AddTransient<AdminRegisterView>();
        builder.Services.AddTransient<CategoryViewModel>();
        builder.Services.AddTransient<CategoryView>();
        builder.Services.AddTransient<AddCategoryViewModel>();
        builder.Services.AddTransient<AddCategory>();
        builder.Services.AddTransient<ProductViewModel>();
        builder.Services.AddTransient<ProductView>();
      
        builder.Services.AddTransient<AddProductViewModel>();
        builder.Services.AddTransient<AddProductView>();
        builder.Services.AddTransient<ShopDetailView>();
        builder.Services.AddTransient<ShopMapViewModel>();
        builder.Services.AddTransient<ShopMapView>();
        builder.Services.AddTransient<SchedulerView>();
        builder.Services.AddTransient<ScheduleViewModel>();   
        builder.Services.AddTransient<EditReminder>();
        builder.Services.AddTransient<EditReminderViewModel>();
        builder.Services.AddTransient<ProductDetailView>();
        builder.Services.AddTransient<ProductDetailViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

