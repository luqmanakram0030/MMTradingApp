


using MMAdmin.Views.EmployeeManagement;

namespace MMAdmin;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            
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
        builder.Services.AddTransient<EmployeeViewModel>();
        builder.Services.AddTransient<EmployeeView>();
        builder.Services.AddTransient<AddEmployeeViewModel>();
        builder.Services.AddTransient<AddEmployee>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

