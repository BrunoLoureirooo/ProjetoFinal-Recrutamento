using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts.Lookups;

namespace PRH.Server.Repository.Lookups;

internal sealed class ResultadoRepository : RepositoryBase<Resultado>, IResultadoRepository
{
    public ResultadoRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<Resultado>> GetResultadosAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }

    public async Task<Resultado> GetResultadoByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(r => r.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> CheckIfResultadoExists(Guid resultadoId, bool resultadoTrackChanges)
    {
        try
        {
            var resultado = await GetResultadoByIdAsync(resultadoId, resultadoTrackChanges);
            return resultado is not null;
        }
        catch
        {
            return false;
        }
    }

    
}
