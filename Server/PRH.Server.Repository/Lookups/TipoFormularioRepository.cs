using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts.Lookups;

namespace PRH.Server.Repository.Lookups;

internal sealed class TipoFormularioRepository : RepositoryBase<TipoFormulario>, ITipoFormularioRepository
{
    public TipoFormularioRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<TipoFormulario>> GetTiposFormularioAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }
}
