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

    public async Task<IEnumerable<Empresa>> GetEmpresasByNameAsync(string name, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Where(c => c.Nome.Contains(name))
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<Empresa> GetEmpresaByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(e => e.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CheckIfEmpresaExists(Guid empresaId, bool empresaTrackChanges)
    {
        try
        {
            var empresa = await GetEmpresaByIdAsync(empresaId, empresaTrackChanges);
            return empresa is not null;
        }
        catch
        {
            return false;
        }
    }
}
