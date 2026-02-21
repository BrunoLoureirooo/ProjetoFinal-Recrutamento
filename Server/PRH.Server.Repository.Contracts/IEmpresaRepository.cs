
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> GetEmpresasAsync(bool trackChanges);
    Task<IEnumerable<Empresa>> GetEmpresasByNameAsync(string name, bool trackChanges);
    Task<Empresa> GetEmpresaByIdAsync(Guid id, bool trackChanges);
    Task<bool> CheckIfEmpresaExists(Guid empresaId, bool empresaTrackChanges);
}
