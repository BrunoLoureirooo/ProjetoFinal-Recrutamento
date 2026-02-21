using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PRH.Client.Wasm.Auth;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.Contracts;

namespace PRH.Client.Wasm.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }


    public async Task<bool> LoginAsync(string email, string password)
    {
        var loginData = new UserForAuthenticationDto() { Username = email, Password = password };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/authentication/login", loginData);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var token = await response.Content.ReadFromJsonAsync<LoginResult>();
        await _localStorage.SetItemAsync("authToken", token.AccessToken);
        await _localStorage.SetItemAsync("refreshToken", token.RefreshToken);

        // Notify authentication state provider
        await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(token.AccessToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return true;

    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }


    public record LoginResult(string AccessToken, string RefreshToken);

}


//    public async Task<LoginResult> Login(LoginModel loginModel)
//         {
//             LoginResult loginResult = new LoginResult();
 
//             try
//             {
//                 HttpResponseMessage response = await _httpClient.PostAsJsonAsync<LoginModel>("api/auth/login", loginModel);
 
//                 if (response.IsSuccessStatusCode)
//                 {
//                     loginResult = response.Content.ReadFromJsonAsync<LoginResult>().Result;
//                 }
//                 else
//                 {
//                     throw new Exception("Credenciais inválidas");
//                 }
 
//                 await _localStorage.SetItemAsync("authToken", loginResult.Token);
 
//                 var authState = ((ApiAuthenticationStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
 
//                 ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticatedAuthState(authState);
 
//                 _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
//             }
//             catch (Exception ex)
//             {
 
//             }
                
 
//             return loginResult;
//         }
 