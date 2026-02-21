using Microsoft.Extensions.Logging;
using PRH.Shared.Contracts;
using PRH.Mobile.Services;
using PRH.Mobile.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using PRH.Mobile.Configuration;
using System.Reflection;



#if ANDROID
    using PRH.Mobile.Platforms.Android;
#endif

namespace PRH.Mobile;

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
            
#if ANDROID
        builder.ConfigureMauiHandlers(handlers =>
           {
               handlers.AddHandler<BlazorWebView, CustomBlazorWebViewHandler>();
           });

#endif
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("PRH.Mobile.appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);

        var settings = builder.Configuration
            .GetSection("Settings")
            .Get<AppSettings>();

        if (settings == null || string.IsNullOrEmpty(settings.ApiUrl))
        {
            throw new InvalidOperationException("A configuração 'Settings:ApiUrl' está ausente ou inválida.");
        }


        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddHttpClient("PRH.Server", client =>
        {
            client.BaseAddress = new Uri(settings.ApiUrl);

        });
        builder.Services.AddTransient<AssinaturaDocumentoService>();

        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();

        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
              .CreateClient("PRH.Server"));

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        builder.Services.AddRadzenComponents();
        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<NotificationService>();

        return builder.Build();
    }
}
