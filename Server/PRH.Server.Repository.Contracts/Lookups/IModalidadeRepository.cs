using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts.Lookups;

public interface IModalidadeRepository
{
    Task<IEnumerable<Modalidade>> GetModalidadesAsync(bool trackChanges);
}
