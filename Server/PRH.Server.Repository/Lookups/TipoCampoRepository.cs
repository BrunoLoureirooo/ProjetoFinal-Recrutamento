using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts.Lookups;

namespace PRH.Server.Repository.Lookups;

internal sealed class TipoCampoRepository : RepositoryBase<TipoCampo>, ITipoCampoRepository
{
    public TipoCampoRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<TipoCampo>> GetTiposCampoAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }

    public async Task<TipoCampo> GetTipoCampoByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(r => r.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }
}
