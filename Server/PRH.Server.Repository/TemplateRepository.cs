using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Entities;

namespace PRH.Server.Repository
{
    internal sealed class TemplateRepository : RepositoryBase<Template>, ITemplateRepository
    {
        public TemplateRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task CreateTemplateAsync(Template template, bool trackChanges)
        {
            Create(template);
            await RepositoryContext.SaveChangesAsync();
        }

        public async Task DeleteTemplateAsync(Guid id)
        {
            var entity = await GetTemplateByIdAsync(id, false);
            Delete(entity);
            await SaveAsync();
        }


        public async Task<IEnumerable<Template>> GetTemplatesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(t => t.TipoDocumento) // Mantém a navegação para TipoDocumento
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task<Template> GetTemplateByIdAsync(Guid id, bool trackChanges)
        {
            var template = await FindByCondition(r => r.Id.Equals(id), trackChanges)
                .Include(t => t.TipoDocumento)
                .FirstOrDefaultAsync();

            if (template == null)
                throw new ArgumentException($"Template com o id '{id}' não encontrado.");

            return template;
        }

        public async Task UpdateTemplateAsync(Template template)
        {
            Update(template);
            await SaveAsync();
        }

        public async Task<IEnumerable<Template>> GetTemplatesByEmpresaIdAsync(Guid empresaId, bool trackChanges)
        {
            return await FindByCondition(t => t.EmpresaID.Equals(empresaId), trackChanges)
                .Include(t => t.TipoDocumento)
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }


    }
}
