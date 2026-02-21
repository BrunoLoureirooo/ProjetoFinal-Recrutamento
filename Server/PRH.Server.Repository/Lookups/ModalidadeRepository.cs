using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts.Lookups;

namespace PRH.Server.Repository.Lookups;

internal sealed class ModalidadeRepository : RepositoryBase<Modalidade>, IModalidadeRepository
{
    public ModalidadeRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<Modalidade>> GetModalidadesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Descricao)
            .ToListAsync();
    }
}
