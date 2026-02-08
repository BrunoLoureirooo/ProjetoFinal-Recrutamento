
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> GetEmpresasAsync(bool trackChanges);
}
