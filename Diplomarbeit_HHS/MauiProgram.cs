using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static Diplomarbeit_HHS.Components.Pages.Anmelden;

namespace Diplomarbeit_HHS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

			builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

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

			builder.Services.AddOptions();

			builder.Services.AddSingleton<Models.AppUserService>();



			//var builder = WebApplication.CreateBuilder(args);

			// Lädt die Konfiguration aus der appsettings.json
			builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			var app = builder.Build();
#endif

			return builder.Build();

		}
    }
}
