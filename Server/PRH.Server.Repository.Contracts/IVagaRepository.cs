using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface IVagaRepository
{
    Task<Vaga> GetVagaByIdAsync(Guid id, bool trackChanges);
    Task<IEnumerable<Vaga>> GetVagasByEmpresaAsync(Guid empresaId, bool trackChanges);
    Task<PagedList<Vaga>> GetVagasByEmpresaPaginatedAsync(Guid empresaId, VagaParameters param, bool trackChanges);
    Task<bool> CheckIfVagaExists(Guid vagaId, bool vagaTrackChanges);
    Task CreateVagaAsync(Vaga vaga, bool trackChanges);
    Task UpdateVagaAsync(Vaga vaga, bool trackChanges);
}
