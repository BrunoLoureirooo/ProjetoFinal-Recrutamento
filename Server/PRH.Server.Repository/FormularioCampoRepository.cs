using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository
{
    internal sealed class FormularioCampoRepository : RepositoryBase<FormularioCampo>, IFormularioCampoRepository
    {
        public FormularioCampoRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }

        public async Task CreateFormularioCampoAsync(FormularioCampo FormularioCampo, bool trackChanges)
        {
            Create(FormularioCampo);
            RepositoryContext.SaveChanges();
        }

        public async Task<FormularioCampo?> GetFormularioCampoByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(fc => fc.Id == id, trackChanges)
                .Include(fc => fc.Formulario)
                .Include(fc => fc.Campo)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateFormularioCampoAsync(FormularioCampo formularioCampo, bool trackChanges)
        {
            if (formularioCampo == null)
                throw new ArgumentNullException(nameof(formularioCampo));

            // Marca a entidade como modificada
            Update(formularioCampo);
            await RepositoryContext.SaveChangesAsync();  // Salva as alterações no banco de dados
        }
    }
}
