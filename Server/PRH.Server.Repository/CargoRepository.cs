using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class CargoRepository : RepositoryBase<Cargo>, ICargoRepository
{
    public CargoRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<Cargo>> GetCargosAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }
    public async Task<Cargo?> GetCargoByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id == id, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<Cargo>> GetCargosPaginatedAsync(CargoParameters param, bool trackChanges)
    {
        IQueryable<Cargo> query = FindAll(trackChanges);
        var cargos = await query
                                .OrderBy(c => c.Nome)
                                .Skip(((int)param.PageNumber - 1) * param.PageSize)
                                .Take(param.PageSize)
                                .ToListAsync();
        var count = await query.CountAsync();

        return new PagedList<Cargo>(cargos, count, (int)param.PageNumber, param.PageSize);
    }

    public async Task CreateCargoAsync(Cargo cargo, bool trackChanges)
    {
        Create(cargo);
        await RepositoryContext.SaveChangesAsync();
    }
}
