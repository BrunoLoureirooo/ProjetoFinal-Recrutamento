using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using PRH.Shared.Auth;

namespace PRH.Mobile.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            var identity = new ClaimsIdentity();
            if (!string.IsNullOrEmpty(token))
            {
                var claims = JwtParser.ParseClaimsFromJwt(token).ToList();
                var exp = claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                if (long.TryParse(exp, out var unix) &&
                    DateTimeOffset.FromUnixTimeSeconds(unix).UtcDateTime > DateTime.UtcNow)
                {
                    identity = new ClaimsIdentity(claims, "jwt");
                }
                else
                {
                    await MarkUserAsLoggedOut();
                }
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                identity.IsAuthenticated
                  ? new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsStringAsync("authToken"))
                  : null;

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsStringAsync("authToken", token);
            var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            var anon = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anon)));
        }
    }
}
