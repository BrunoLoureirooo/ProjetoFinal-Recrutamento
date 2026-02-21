using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Radzen;
using Blazored.LocalStorage;

using PRH.Client.Wasm;
using PRH.Client.Wasm.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Client.Wasm.Services;
using PRH.Client.Wasm.Services.FilterState;
using PRH.Shared.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// IP do servidor API
string baseAdress = builder.Configuration.GetValue<string>("ApiUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAdress) });

builder.Services.AddRadzenComponents();


builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();
builder.Services.AddScoped<IRecrutamentoService, RecrutamentoService>();
builder.Services.AddScoped<IEntrevistaService, EntrevistaService>();
builder.Services.AddScoped<ICandidaturaService, CandidaturaService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IFormularioService, FormularioService>();
builder.Services.AddScoped<IVagaService, VagaService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IFormularioCampoService, FormularioCampoService>();
builder.Services.AddScoped<ICampoService, CampoService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IAdmissaoService, AdmissaoService>();

// Filter State Scopes
builder.Services.AddScoped<RecrutamentoFilterStateService>();
builder.Services.AddScoped<CandidaturaFilterStateService>();
builder.Services.AddScoped<VagaFilterStateService>();
builder.Services.AddScoped<EntrevistaFilterStateService>();
builder.Services.AddScoped<AdmissaoFilterStateService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(); 

// builder.Services.AddRadzenCookieThemeService(options =>
// {
//     options.Name = "MyApplicationTheme"; // The name of the cookie
//     options.Duration = TimeSpan.FromDays(365); // The duration of the cookie
// });

await builder.Build().RunAsync();
