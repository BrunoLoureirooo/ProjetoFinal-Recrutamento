using PRH.Server.Entities.Models;
 
 namespace PRH.Server.Repository.Contracts
 {
     public interface ITemplateRepository
     {

        Task CreateTemplateAsync(Template template, bool trackChanges);

        Task<IEnumerable<Template>> GetTemplatesAsync(bool trackChanges);

        Task<Template> GetTemplateByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Template>> GetTemplatesByEmpresaIdAsync(Guid empresaId, bool trackChanges);

         Task UpdateTemplateAsync(Template template);

        Task DeleteTemplateAsync(Guid id);

        Task SaveAsync();
     }
 }