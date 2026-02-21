using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class EntrevistaRepository : RepositoryBase<Entrevista>, IEntrevistaRepository
{
    public EntrevistaRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<Entrevista> GetEntrevistaByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(e => e.Id.Equals(id), trackChanges)
                     .FirstOrDefaultAsync();
    }

    public async Task<Guid> GetEmpresaOfEntrevistaByIdAsync(Guid id, bool trackChanges)
    {
        var entrevista = await FindByCondition(e => e.Id.Equals(id), trackChanges)
                              .FirstOrDefaultAsync();

        return entrevista is not null
            ? entrevista.Candidatura.Vaga.Recrutamento.Empresa.Id
            : Guid.Empty;
    }

    public async Task<IEnumerable<Entrevista>> GetEntrevistasAsync(EntrevistaParameters param, bool trackChanges)
    {
        var query = CreateQueryFromParams(param, trackChanges);

        return await query
                     .OrderByDescending(e => e.DataHora)
                     .ToListAsync();
    }

    public async Task<PagedList<Entrevista>> GetEntrevistasPaginatedAsync(EntrevistaParameters param, bool trackChanges)
    {
        var query = CreateQueryFromParams(param, trackChanges);

        var entrevistas = await query
                             .OrderByDescending(e => e.DataHora)
                             .Skip(((int)param.PageNumber - 1) * param.PageSize)
                             .Take(param.PageSize)
                             .ToListAsync();

        var count = await query.CountAsync();

        return new PagedList<Entrevista>(entrevistas, count, (int)param.PageNumber, param.PageSize);
    }

    public IQueryable<Entrevista> CreateQueryFromParams(EntrevistaParameters param, bool trackChanges)
    {
        var query = FindAll(trackChanges);

        if (!string.IsNullOrWhiteSpace(param.NomeCandidato))
            query = query.Where(e => e.Candidatura.NomeCandidato.ToLower().Contains(param.NomeCandidato.ToLower()));

        if (param.ResultadoId is not null)
            query = query.Where(e => e.Resultado.Id.Equals(param.ResultadoId));

        if (param.EmpresaId is not null)
            query = query.Where(e => e.Candidatura.Vaga.Recrutamento.Empresa.Id.Equals(param.EmpresaId));

        if (param.Before is not null)
            query = query.Where(e => e.DataHora <= param.Before);

        if (param.After is not null)
            query = query.Where(e => e.DataHora >= param.After);

        return query;
    }

    

    public async Task CreateEntrevistaAsync(Entrevista entrevista, bool trackChanges)
    {
        Create(entrevista);
        await RepositoryContext.SaveChangesAsync();
    }

    public async Task UpdateEntrevistaAsync(Entrevista entrevista, bool trackChanges)
    {
        if (!trackChanges)
        {
            Update(entrevista); // Marca como modified apenas se não está sendo rastreado
        }

        await RepositoryContext.SaveChangesAsync();
    }
}
