using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository
{
	internal sealed class FormularioRepository : RepositoryBase<Formulario>, IFormularioRepository
	{
		public FormularioRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
		{

		}

        public async Task CreateFormularioAsync(Formulario Formulario, bool trackChanges)
		{
			Create(Formulario);
			RepositoryContext.SaveChanges();
		}
        public async Task UpdateFormularioAsync(Formulario formulario)
        {
            // Attach the entity if it's not already being tracked
            RepositoryContext.Formularios.Update(formulario);

            // Save changes
            await RepositoryContext.SaveChangesAsync();
        }


        public async Task<Formulario> GetFormularioByIdAsync(Guid id, bool trackChanges)
		{
			return await FindByCondition(f => f.Id.Equals(id), trackChanges)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Formulario>> GetFormulariosAsync(bool trackChanges)
		{
			return await FindAll(trackChanges)
				.OrderBy(f => f.Nome)
				.ToListAsync();
		}

        public async Task<PagedList<Formulario>> GetFormulariosPaginatedAsync(int page, int pageSize, bool trackChanges)
        {
            var formularios = await FindAll(trackChanges)
                                        .OrderBy(f => f.Nome)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Formulario>(formularios, count, page, pageSize);
        }
    }
}
