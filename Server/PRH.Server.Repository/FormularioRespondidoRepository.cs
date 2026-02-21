using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository
{
	internal sealed class FormularioRespondidoRepository : RepositoryBase<FormularioRespondido>, IFormularioRespondidoRepository
	{
		public FormularioRespondidoRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
		{

		}

        public async Task<IEnumerable<FormularioRespondido>> GetRespostasByCandidaturaAsync(Guid candidaturaId, bool trackChanges)
        {
            return await FindByCondition(fr => fr.Candidatura.Id.Equals(candidaturaId), trackChanges)
                        .OrderBy(fr => fr.Id)
                        .ToListAsync();
        }
    }
}
