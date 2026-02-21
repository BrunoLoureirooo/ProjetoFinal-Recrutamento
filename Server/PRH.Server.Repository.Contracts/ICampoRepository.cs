using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface ICampoRepository
{
    Task<IEnumerable<Campo>> GetCamposAsync(bool trackChanges);
    Task<IEnumerable<Campo>> GetCamposByVagaAsync(Guid vagaId, bool trackChanges);
    Task CreateCampoAsync(Campo campo, bool trackChanges);
    Task<List<string>> GetCamposByNomeUnico(string nomeUnicoBase);
}
