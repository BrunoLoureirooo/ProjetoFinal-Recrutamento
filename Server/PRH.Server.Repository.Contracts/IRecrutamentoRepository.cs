using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface IRecrutamentoRepository
{
    Task<IEnumerable<Recrutamento>> GetRecrutamentosAsync(RecrutamentoParameters param, bool trackChanges);
    Task<Recrutamento> GetRecrutamentoByIdAsync(Guid id, bool trackChanges);
    Task CreateRecrutamentoAsync(Recrutamento recrutamento, bool trackChanges);
    Task<PagedList<Recrutamento>> GetRecrutamentosPaginatedAsync(RecrutamentoParameters param, bool trackChanges);
    IQueryable<Recrutamento> CreateQueryFromParams(RecrutamentoParameters param, bool trackChanges);
    Task UpdateRecrutamentoAsync(Recrutamento recrutamento, bool trackChanges);
    Task<bool> CheckIfRecrutamentoExists(Guid recrutamentoId, bool recrutamentoTrackChanges);
}
