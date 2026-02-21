using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository;

internal sealed class GestorClienteRepository : RepositoryBase<GestorCliente>, IGestorClienteRepository
{
    public GestorClienteRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }

    public async Task<GestorCliente> GetGestorByUserIdAsync(string userId, bool trackChanges)
    {
        return await FindByCondition(g => g.ApplicationUserId.Equals(userId), trackChanges)
            .FirstOrDefaultAsync();
    }
}
