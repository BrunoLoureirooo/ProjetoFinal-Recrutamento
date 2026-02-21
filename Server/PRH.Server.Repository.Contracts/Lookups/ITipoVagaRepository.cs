using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts.Lookups;

public interface ITipoVagaRepository
{
    Task<IEnumerable<TipoVaga>> GetTiposVagaAsync(bool trackChanges);
}
