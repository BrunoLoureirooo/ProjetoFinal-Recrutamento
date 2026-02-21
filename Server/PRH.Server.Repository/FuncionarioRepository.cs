using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }

    public async Task<Funcionario> GetFuncionarioByUserIdAsync(string userId, bool trackChanges)
    {
        return await FindByCondition(f => f.ApplicationUserId.Equals(userId), trackChanges)
            .FirstOrDefaultAsync();
    }
    public async Task<Funcionario> GetFuncionarioByIdAsync(Guid Id, bool trackChanges)
    {
        return await FindByCondition(f => f.Id.Equals(Id), trackChanges)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(f => f.ApplicationUser.Nome)
            .ToListAsync();
    }

    public async Task<PagedList<Funcionario>> GetFuncionariosPaginatedAsync(int page, int pageSize, bool trackChanges)
    {
        var Funcionarios = await FindAll(trackChanges)
                                    .OrderBy(f => f.ApplicationUser.Nome)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Funcionario>(Funcionarios, count, page, pageSize);
    }

    public async Task<IEnumerable<Funcionario>> GetFuncionariosByEmpresaAsync(Guid empresaId, bool trackChanges)
    {
        return await FindByCondition(f => f.EmpresaId.Equals(empresaId), trackChanges)
                        .OrderBy(f => f.ApplicationUser.Nome)
                        .ToListAsync();
    }
}
