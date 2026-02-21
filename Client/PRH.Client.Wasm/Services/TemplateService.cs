using System.Net.Http.Json;
 
 using PRH.Shared.DataTransferObjets;
 using PRH.Client.Wasm.Services.Contracts;
namespace PRH.Client.Wasm.Services
{
     public class TemplateService : ITemplateService
     {
         private readonly HttpClient _httpClient;
 
         public TemplateService(HttpClient httpClient)
         {
             _httpClient = httpClient;
         }

        public async Task CreateTemplateAsync(TemplateDto dto)
        {
            await _httpClient.PostAsJsonAsync("api/templates", dto);
        }


        public async Task<IEnumerable<TemplateDto>> GetTemplatesAsync()
         {
             return await _httpClient.GetFromJsonAsync<IEnumerable<TemplateDto>>("api/templates");
         }

        public async Task<TemplateDto> GetTemplateByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<TemplateDto>("api/templates/" + id);
        }


        public async Task UpdateTemplateAsync(Guid id, TemplateDto dto)
        {
            await _httpClient.PatchAsJsonAsync($"api/templates/{id}", dto);
        }

        public async Task<IEnumerable<TemplateDto>> GetTemplatesByEmpresaIdAsync(Guid empresaId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TemplateDto>>($"api/templates/empresa/{empresaId}");
        }
        
        public Task DeleteTemplateAsync(Guid id)
      => _httpClient.DeleteAsync($"api/templates/{id}");


    }
 }