using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PRH.Shared.DataTransferObjets;
using PRH.Client.Wasm.Services.Contracts;

namespace PRH.Client.Wasm.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
{
    private readonly HttpClient _httpClient;
    public TipoDocumentoService(HttpClient httpClient) => _httpClient = httpClient;

    public Task CreateTipoDocumentoAsync(TipoDocumentoDto dto)
        => _httpClient.PostAsJsonAsync("api/tipodocumentos", dto);

    public Task<IEnumerable<TipoDocumentoDto>> GetTipoDocumentosAsync()
        => _httpClient.GetFromJsonAsync<IEnumerable<TipoDocumentoDto>>("api/tipodocumentos");

    public Task<TipoDocumentoDto> GetTipoDocumentoByIdAsync(Guid id)
        => _httpClient.GetFromJsonAsync<TipoDocumentoDto>($"api/tipodocumentos/{id}");

    public Task UpdateTipoDocumentoAsync(Guid id, TipoDocumentoDto dto)
        => _httpClient.PatchAsJsonAsync($"api/tipodocumentos/{id}", dto);

    public Task DeleteTipoDocumentoAsync(Guid id)
        => _httpClient.DeleteAsync($"api/tipodocumentos/{id}");
}
}
