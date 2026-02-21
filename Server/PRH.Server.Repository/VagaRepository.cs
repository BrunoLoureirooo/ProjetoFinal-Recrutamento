using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class VagaRepository : RepositoryBase<Vaga> , IVagaRepository
{
    public VagaRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {

    }

    public async Task<Vaga> GetVagaByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(v => v.Id.Equals(id), trackChanges)
                .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Vaga>> GetVagasByEmpresaAsync(Guid empresaId, bool trackChanges)
    {
        return await FindByCondition(v => v.Recrutamento.Empresa.Id.Equals(empresaId), trackChanges)
                        .OrderByDescending(v => v.Recrutamento.Data)
                        .ToListAsync();
    }

    public async Task<PagedList<Vaga>> GetVagasByEmpresaPaginatedAsync(Guid empresaId, VagaParameters param, bool trackChanges)
    {
        IQueryable<Vaga> query = FindByCondition(v => v.Recrutamento.Empresa.Id == empresaId, trackChanges);

        var vagas = await query
            .OrderByDescending(v => v.Recrutamento.Data)
            .Skip(((int)param.PageNumber - 1) * param.PageSize)
            .Take(param.PageSize)
            .ToListAsync();

        // Separar a contagem pode reduzir o risco de timeout
        var count = await FindByCondition(v => v.Recrutamento.Empresa.Id == empresaId, false).CountAsync();

        return new PagedList<Vaga>(vagas, count, (int)param.PageNumber, param.PageSize);
    }


    public async Task<bool> CheckIfVagaExists(Guid vagaId, bool vagaTrackChanges)
    {
        try
        {
            var vaga = await GetVagaByIdAsync(vagaId, vagaTrackChanges);
            return vaga is not null;
        }
        catch
        {
            return false;
        }
    }

    public async Task CreateVagaAsync(Vaga vaga, bool trackChanges)
    {
        Create(vaga);
        await RepositoryContext.SaveChangesAsync();
    }

    public async Task UpdateVagaAsync(Vaga vaga, bool trackChanges)
    {
        if (!trackChanges)
        {
            Update(vaga); // Marca como modified apenas se não está sendo rastreado
        }

        await RepositoryContext.SaveChangesAsync();
    }

}
