using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts.Lookups;

namespace PRH.Server.Repository.Lookups;

internal sealed class EstadoRepository : RepositoryBase<Estado>, IEstadoRepository
{
    public EstadoRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }

    public async Task<bool> CheckIfEstadoExists(Guid estadoId, bool estadoTrackChanges)
    {
        try
        {
            var estado = await GetEstadoByIdAsync(estadoId, estadoTrackChanges);
            return estado is not null;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Estado> GetEstadoByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(e => e.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Estado>> GetEstadosAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }
}
