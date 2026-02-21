using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository
{
    internal sealed class CampoRepository : RepositoryBase<Campo>, ICampoRepository
    {
        public CampoRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Campo>> GetCamposAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(r => r.TipoCampo)
                .ToListAsync();
        }

        public async Task CreateCampoAsync(Campo campo, bool trackChanges)
        {
            Create(campo);
            await RepositoryContext.SaveChangesAsync();
        }
        public async Task<List<string>> GetCamposByNomeUnico(string nomeUnicoBase)
        {
            return await FindByCondition(c => c.NomeUnico.StartsWith(nomeUnicoBase), trackChanges: false)
                .Select(c => c.NomeUnico)
                .ToListAsync();
        }

        public async Task<IEnumerable<Campo>> GetCamposByVagaAsync(Guid vagaId, bool trackChanges)
        {
            return await FindByCondition(c => c.FormularioCampos.Any(fc => fc.Formulario.VagaFormularios.Any(vf => vf.Vaga.Id.Equals(vagaId))), trackChanges)
                        .OrderBy(c => c.Nome)
                        .ToListAsync();
        }
    }
}
