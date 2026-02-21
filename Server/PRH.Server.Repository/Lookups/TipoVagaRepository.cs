using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts.Lookups;

namespace PRH.Server.Repository.Lookups;

internal sealed class TipoVagaRepository : RepositoryBase<TipoVaga>, ITipoVagaRepository
{
    public TipoVagaRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<TipoVaga>> GetTiposVagaAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }
}
