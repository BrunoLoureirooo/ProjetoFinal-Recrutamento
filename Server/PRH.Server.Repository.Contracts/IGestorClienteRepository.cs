using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface IGestorClienteRepository
{
    Task<GestorCliente> GetGestorByUserIdAsync(string userId, bool trackChanges);
}
