using System;
using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository;

internal sealed class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext)
    {

    }

    public async Task<IEnumerable<Empresa>> GetEmpresasAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }
}
