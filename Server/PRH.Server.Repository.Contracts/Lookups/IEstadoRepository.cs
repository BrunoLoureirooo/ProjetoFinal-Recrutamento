using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts.Lookups;

public interface IEstadoRepository
{
    Task<Estado> GetEstadoByIdAsync(Guid id, bool trackChanges);
    Task<bool> CheckIfEstadoExists(Guid estadoId, bool estadoTrackChanges);
    Task<IEnumerable<Estado>> GetEstadosAsync(bool trackChanges);
}
