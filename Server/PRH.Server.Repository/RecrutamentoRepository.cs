using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class RecrutamentoRepository : RepositoryBase<Recrutamento>, IRecrutamentoRepository
{
    public RecrutamentoRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }

    public async Task CreateRecrutamentoAsync(Recrutamento recrutamento, bool trackChanges)
    {
        Create(recrutamento);
        await RepositoryContext.SaveChangesAsync();
    }

    public async Task<Recrutamento> GetRecrutamentoByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(r => r.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Recrutamento>> GetRecrutamentosAsync(RecrutamentoParameters param, bool trackChanges)
    {

        var query = CreateQueryFromParams(param, trackChanges);

        return await query
                    .OrderByDescending(r => r.Data)
                    .ToListAsync();
    }

    public async Task<PagedList<Recrutamento>> GetRecrutamentosPaginatedAsync(RecrutamentoParameters param, bool trackChanges)
    {
        var query = CreateQueryFromParams(param, trackChanges)
                        .AsNoTracking();
        var recrutamentos = await query
                                .OrderByDescending(r => r.Data)
                                .Skip(((int)param.PageNumber - 1) * param.PageSize)
                                .Take(param.PageSize)
                                .ToListAsync();
        var count = await query.CountAsync();

        return new PagedList<Recrutamento>(recrutamentos, count, (int)param.PageNumber, param.PageSize);
    }

    public IQueryable<Recrutamento> CreateQueryFromParams(RecrutamentoParameters param, bool trackChanges)
    {
        IQueryable<Recrutamento> query = FindAll(trackChanges);

        if (param.Nome is not null)
            query = query.Where(r => r.Nome.ToLower().Contains(param.Nome.ToLower()));

        if (param.EmpresaId is not null)
            query = query.Where(r => r.Empresa.Id.Equals(param.EmpresaId));

        if (param.Before is not null)
            query = query.Where(r => r.Data <= param.Before);

        if (param.After is not null)
            query = query.Where(r => r.Data >= param.After);

        if (param.CandidaturasDisponiveis is not null)
        {
            if ((bool)param.CandidaturasDisponiveis)
                query = query.Where(r => r.Vagas.Count(v => v.CandidaturaDisponivel) >= 1);

            else
                query = query.Where(r => r.Vagas.Count(v => v.CandidaturaDisponivel) == 0);
        }

        return query;
    }

    public async Task UpdateRecrutamentoAsync(Recrutamento recrutamento, bool trackChanges)
    {
        Update(recrutamento);
        await RepositoryContext.SaveChangesAsync();
    }

    public async Task<bool> CheckIfRecrutamentoExists(Guid recrutamentoId, bool recrutamentoTrackChanges)
    {
        try
        {
            var recrutamento = await GetRecrutamentoByIdAsync(recrutamentoId, recrutamentoTrackChanges);
            return recrutamento is not null;
        }
        catch
        {
            return false;
        }
    }
}
