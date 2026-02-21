using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface ICandidaturaRepository
{
    Task<IEnumerable<Candidatura>> GetCandidaturasAsync(CandidaturaParameters param, bool trackChanges);
    Task<Candidatura> GetCandidaturaByIdAsync(Guid id, bool trackChanges);
    Task CreateCandidaturaAsync(Candidatura candidatura, bool trackChanges);
    Task<PagedList<Candidatura>> GetCandidaturasPaginatedAsync(CandidaturaParameters param, bool trackChanges);
    Task<IEnumerable<Candidatura>> GetCandidaturasByEmpresaAsync(Guid empresaId, CandidaturaParameters param, bool trackChanges);
    Task<PagedList<Candidatura>> GetCandidaturasByEmpresaPaginatedAsync(Guid empresaId, CandidaturaParameters param, bool trackChanges);
    Task<bool> CheckIfCandidaturaExists(Guid candidaturaId, bool vagaTrackChanges);
    Task UpdateCandidaturaAsync(Candidatura candidatura, bool trackChanges);
    Task<PagedList<Candidatura>> GetAdmissaoPaginatedAsync(AdmissaoParameters param, bool trackChanges);
    Task<IEnumerable<Candidatura>> GetAdmissaoAsync(AdmissaoParameters param, bool trackChanges);
}
