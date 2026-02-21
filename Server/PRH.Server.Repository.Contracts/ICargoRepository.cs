using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface ICargoRepository
{
    Task<IEnumerable<Cargo>> GetCargosAsync(bool trackChanges);
    Task<Cargo?> GetCargoByIdAsync(Guid id, bool trackChanges);
    Task<PagedList<Cargo>> GetCargosPaginatedAsync(CargoParameters param, bool trackChanges);
    Task CreateCargoAsync(Cargo cargo, bool trackChanges);
}
