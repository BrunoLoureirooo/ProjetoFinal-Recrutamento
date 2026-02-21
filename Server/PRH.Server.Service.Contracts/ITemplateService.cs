using PRH.Server.Entities.Models;
 using PRH.Shared.DataTransferObjets;
 
 namespace PRH.Server.Service.Contracts
 {
     public interface ITemplateService
     {
        Task<TemplateDto> CreateTemplate(TemplateDto dto, bool trackChanges);
        Task<IEnumerable<TemplateDto>> GetTemplates(bool trackChanges);

        Task<TemplateDto> GetTemplateById(Guid id, bool trackChanges);
        Task<IEnumerable<TemplateDto>> GetTemplatesByEmpresaId(Guid empresaId, bool trackChanges);
        Task UpdateTemplateAsync(Guid id, TemplateDto dto);
        Task DeleteTemplateAsync(Guid id);
    }
 }