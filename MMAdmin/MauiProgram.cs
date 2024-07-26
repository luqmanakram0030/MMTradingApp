


namespace MMAdmin;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
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
        builder.Services.AddScoped<IAdminUser, AdminUserService>();
        builder.Services.AddTransient<AdminLoginView>();
        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<AdminLoginViewModel>();
        builder.Services.AddTransient <AdminRegisterViewModel>();
        builder.Services.AddTransient <AdminRegisterView>();
        
        builder.Services.AddTransient<EmployeeView>();
        
        builder.Services.AddTransient<AddEmployee>();
        // Register services
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
        builder.Services.AddTransient<EmployeesViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

