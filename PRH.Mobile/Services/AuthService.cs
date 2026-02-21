using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PRH.Shared.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Mobile.Auth;
using System.Diagnostics;

namespace PRH.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient,
                           ILocalStorageService localStorage,
                           AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var dto = new UserForAuthenticationDto { Username = email, Password = password };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/authentication/login", dto);
                if (!response.IsSuccessStatusCode) return false;

                var token = await response.Content.ReadFromJsonAsync<LoginResult>();
                await _localStorage.SetItemAsync("authToken", token.AccessToken);
                await _localStorage.SetItemAsync("refreshToken", token.RefreshToken);

                await ((CustomAuthStateProvider)_authStateProvider)
                      .MarkUserAsAuthenticated(token.AccessToken);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);

                return true;
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"[Login] HTTP error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Login] Unexpected error: {ex}");
                return false;
            }

        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public record LoginResult(string AccessToken, string RefreshToken);
}
