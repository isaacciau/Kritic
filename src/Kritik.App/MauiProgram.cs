using Microsoft.Extensions.Logging;

namespace Kritik.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        
        // Configure HttpClient for Kritik.Backend
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.0.130:5229/") });

        builder.Services.AddScoped<Services.ValidationService>();

        builder.Services.AddScoped<Services.ProjectService>();
        builder.Services.AddScoped<Services.EvaluationService>();
        builder.Services.AddScoped<Services.ToastService>();
        builder.Services.AddScoped<Services.AuthService>();

		return builder.Build();
	}
}
