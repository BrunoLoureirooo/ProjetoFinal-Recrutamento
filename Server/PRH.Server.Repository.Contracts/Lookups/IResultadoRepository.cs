using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts.Lookups;

public interface IResultadoRepository
{
    Task<IEnumerable<Resultado>> GetResultadosAsync(bool trackChanges);
    Task<Resultado> GetResultadoByIdAsync(Guid id, bool trackChanges);
    Task<bool> CheckIfResultadoExists(Guid resultadoId, bool resultadoTrackChanges);
}
