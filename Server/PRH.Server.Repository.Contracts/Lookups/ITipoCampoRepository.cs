using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts.Lookups;

public interface ITipoCampoRepository
{
    Task<IEnumerable<TipoCampo>> GetTiposCampoAsync(bool trackChanges);
    Task<TipoCampo> GetTipoCampoByIdAsync(Guid id, bool trackChanges);
}
