using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface IEntrevistaRepository
{
    Task<Entrevista> GetEntrevistaByIdAsync(Guid id, bool trackChanges);

    Task<Guid> GetEmpresaOfEntrevistaByIdAsync(Guid id, bool trackChanges);

    Task<IEnumerable<Entrevista>> GetEntrevistasAsync(EntrevistaParameters param, bool trackChanges);

    Task<PagedList<Entrevista>> GetEntrevistasPaginatedAsync(EntrevistaParameters param, bool trackChanges);

    Task CreateEntrevistaAsync(Entrevista entrevista, bool trackChanges);
    Task UpdateEntrevistaAsync(Entrevista entrevista, bool trackChanges);
}
