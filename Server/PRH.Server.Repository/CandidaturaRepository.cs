using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class CandidaturaRepository : RepositoryBase<Candidatura>, ICandidaturaRepository
{
    public CandidaturaRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }

    public async Task CreateCandidaturaAsync(Candidatura candidatura, bool trackChanges)
    {
        Create(candidatura);
        await RepositoryContext.SaveChangesAsync();
    }

    public async Task<Candidatura> GetCandidaturaByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Candidatura>> GetCandidaturasAsync(CandidaturaParameters param, bool trackChanges)
    {
        var query = CreateQueryFromCandidaturaParams(param, trackChanges);

        return await query
                    .OrderBy(c => c.NomeCandidato)
                    .ToListAsync();
    }

    public async Task<PagedList<Candidatura>> GetCandidaturasPaginatedAsync(CandidaturaParameters param, bool trackChanges)
    {
        var query = CreateQueryFromCandidaturaParams(param, trackChanges);
        var candidaturas = await query
                                .OrderBy(c => c.NomeCandidato)
                                .Skip(((int)param.PageNumber - 1) * param.PageSize)
                                .Take(param.PageSize)
                                .ToListAsync();
        var count = await query.CountAsync();

        return new PagedList<Candidatura>(candidaturas, count, (int)param.PageNumber, param.PageSize);
    }

    public async Task<PagedList<Candidatura>> GetAdmissaoPaginatedAsync(AdmissaoParameters param, bool trackChanges)
    {
        var query = CreateQueryFromAdmissaoParams(param, trackChanges);

        var candidaturas = await query
                             .OrderBy(c => c.NomeCandidato)
                             .Skip(((int)param.PageNumber - 1) * param.PageSize)
                             .Take(param.PageSize)
                             .ToListAsync();

        var count = await query.CountAsync();

        return new PagedList<Candidatura>(candidaturas, count, (int)param.PageNumber, param.PageSize);
    }

    public async Task<IEnumerable<Candidatura>> GetAdmissaoAsync(AdmissaoParameters param, bool trackChanges)
    {
        var query = CreateQueryFromAdmissaoParams(param, trackChanges);

        return await query
            .OrderBy(c => c.NomeCandidato)
            .ToListAsync();
    }

    private IQueryable<Candidatura> CreateQueryFromCandidaturaParams(CandidaturaParameters param, bool trackChanges)
    {
        IQueryable<Candidatura> query = FindAll(trackChanges);

        if (param.RecrutamentoNome is not null)
            query = query.Where(c => c.Vaga.Recrutamento.Nome.ToLower().Contains(param.RecrutamentoNome.ToLower()));

        if (param.CandidatoNome is not null)
            query = query.Where(c => c.NomeCandidato.ToLower().Contains(param.CandidatoNome.ToLower()));

        if (param.EstadoId is not null)
            query = query.Where(c => c.Estado.Id.Equals(param.EstadoId));

        if (param.EmpresaId is not null)
            query = query.Where(c => c.Vaga.Recrutamento.Empresa.Id.Equals(param.EmpresaId));

        return query;
    }

    private IQueryable<Candidatura> CreateQueryFromAdmissaoParams(AdmissaoParameters param, bool trackChanges)
    {
        IQueryable<Candidatura> query = FindByCondition(
            e => e.Estado != null && e.Estado.Descricao == "Admitido",
            trackChanges);

        if (param.RecrutamentoNome is not null)
            query = query.Where(c => c.Vaga.Recrutamento.Nome.ToLower().Contains(param.RecrutamentoNome.ToLower()));

        if (param.CandidatoNome is not null)
            query = query.Where(c => c.NomeCandidato.ToLower().Contains(param.CandidatoNome.ToLower()));

        if (param.EmpresaId is not null)
            query = query.Where(c => c.Vaga.Recrutamento.Empresa.Id.Equals(param.EmpresaId));

        return query;
    }

    public async Task<IEnumerable<Candidatura>> GetCandidaturasByEmpresaAsync(Guid empresaId, CandidaturaParameters param, bool trackChanges)
    {
        return await FindByCondition(c => c.Vaga.Recrutamento.Empresa.Id.Equals(empresaId), trackChanges)
                        .OrderByDescending(c => c.Vaga.Recrutamento.Data)
                        .ToListAsync();
    }

    public async Task<PagedList<Candidatura>> GetCandidaturasByEmpresaPaginatedAsync(Guid empresaId, CandidaturaParameters param, bool trackChanges)
    {
        IQueryable<Candidatura> query = FindByCondition(c => c.Vaga.Recrutamento.Empresa.Id.Equals(empresaId), trackChanges);
        var candidaturas = await query
                            .OrderByDescending(c => c.Vaga.Recrutamento.Data)
                            .Skip(((int)param.PageNumber - 1) * param.PageSize)
                            .Take(param.PageSize)
                            .ToListAsync();
        var count = await query.CountAsync();

        return new PagedList<Candidatura>(candidaturas, count, (int)param.PageNumber, param.PageSize);
    }

    public async Task<bool> CheckIfCandidaturaExists(Guid candidaturaId, bool vagaTrackChanges)
    {
        try
        {
            var candidatura = await GetCandidaturaByIdAsync(candidaturaId, vagaTrackChanges);
            return candidatura is not null;
        }
        catch
        {
            return false;
        }
    }

    public async Task UpdateCandidaturaAsync(Candidatura candidatura, bool trackChanges)
    {
        if (!trackChanges)
        {
            Update(candidatura); // Marca como modified apenas se não está sendo rastreado
        }

        await RepositoryContext.SaveChangesAsync();
    }
}
