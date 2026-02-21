using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets;
using System.Net;
using System.Net.Http;

namespace PRH.Client.Wasm.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly HttpClient _httpClient;

        public DocumentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateDocumentoAsync(DocumentoDto dto)
        {
            await _httpClient.PostAsJsonAsync("api/admissoes/documentos/createdocumento", dto);
        }

        public Task<IEnumerable<DocumentoDto>> GetDocumentosByCandidaturaIdAsync(Guid candidaturaId)
            => _httpClient.GetFromJsonAsync<IEnumerable<DocumentoDto>>(
                 $"api/admissoes/documentos?candidaturaId={candidaturaId}");

        public Task<DocumentoDto> GetDocumentoByIdAsync(Guid id)
            => _httpClient.GetFromJsonAsync<DocumentoDto>(
                 $"api/admissoes/documentos/{id}");

        public async Task UpdateDocumentoAsync(Guid id, DocumentoDto dto)
        { await _httpClient.PatchAsJsonAsync($"api/admissoes/documentos/{id}", dto); }


        public async Task<DocumentoDto> CreateDocumentoFromTemplateAsync(
    Guid candidaturaId,
    Guid templateId,
    Dictionary<string, string>? extras = null)
        {
            var url = $"api/admissoes/documentos/template?candidaturaId={candidaturaId}&templateId={templateId}";
            HttpResponseMessage resp;

            if (extras == null)
                resp = await _httpClient.PostAsync(url, null);
            else
                resp = await _httpClient.PostAsJsonAsync(url, extras);

            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                var faltantes = await resp.Content.ReadFromJsonAsync<string[]>();
                throw new PlaceholderMissingException(faltantes!);
            }

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<DocumentoDto>()!;
        }

        public Task DeleteDocumentoAsync(Guid id)
            => _httpClient.DeleteAsync($"api/admissoes/documentos/{id}");

        public Task SignDocumentoDigitalAsync(Guid id)
            => _httpClient.PatchAsync($"api/admissoes/documentos/{id}/sign-digital", null);

        public Task SignDocumentoFisicoAsync(Guid id)
            => _httpClient.PatchAsync($"api/admissoes/documentos/{id}/sign-fisico", null);

        
        
    }
      

    public class PlaceholderMissingException : Exception
    {
        public string[] CamposFaltantes { get; }
        public PlaceholderMissingException(string[] campos)
            : base("Faltam valores para placeholders.") =>
            CamposFaltantes = campos;
    }

      

}