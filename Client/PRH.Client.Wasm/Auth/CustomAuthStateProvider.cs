using System;
using PRH.Shared.Auth;

namespace PRH.Client.Wasm.Auth;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

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

        ClaimsIdentity identity = new();

        if (!string.IsNullOrEmpty(token))
        {
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var expiracyClaim = claims.FirstOrDefault(c => c.Type == "exp");
            if (expiracyClaim != null && long.TryParse(expiracyClaim.Value, out long expiracyUnix))
            {
                var expiracyDate = DateTimeOffset.FromUnixTimeSeconds(expiracyUnix).UtcDateTime;
                
                if(expiracyDate <= DateTime.UtcNow)
                    await MarkUserAsLoggedOut();
                
                else
                    identity = new ClaimsIdentity(claims, "jwt");
                
            }
        }

        var user = new ClaimsPrincipal(identity);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await _localStorage.SetItemAsStringAsync("authToken", token);
        var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("authToken");
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
}
