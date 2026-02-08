using System;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IAuthService
{
    Task<bool> LoginAsync(string email, string password);
    Task LogoutAsync();
}
