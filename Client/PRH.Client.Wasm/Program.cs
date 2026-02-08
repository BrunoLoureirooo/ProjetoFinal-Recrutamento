using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

using Radzen;
using Blazored.LocalStorage;

using PRH.Client.Wasm;
using PRH.Client.Wasm.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Client.Wasm.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });

builder.Services.AddRadzenComponents();


builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(); // Ensure you implement this

// builder.Services.AddRadzenCookieThemeService(options =>
// {
//     options.Name = "MyApplicationTheme"; // The name of the cookie
//     options.Duration = TimeSpan.FromDays(365); // The duration of the cookie
// });

await builder.Build().RunAsync();
