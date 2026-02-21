using PRH.Shared.DataTransferObjets;
 
 namespace PRH.Client.Wasm.Services.Contracts
 {
     public interface ITemplateService
     {
        Task CreateTemplateAsync(TemplateDto dto);
        Task<IEnumerable<TemplateDto>> GetTemplatesAsync();

        Task<TemplateDto> GetTemplateByIdAsync(Guid id);

        Task UpdateTemplateAsync(Guid id, TemplateDto dto);

        Task<IEnumerable<TemplateDto>> GetTemplatesByEmpresaIdAsync(Guid empresaId);

        Task DeleteTemplateAsync(Guid id);

    }
 }