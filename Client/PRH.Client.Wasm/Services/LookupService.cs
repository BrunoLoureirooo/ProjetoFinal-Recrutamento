using System.Net.Http.Json;

using PRH.Shared.DataTransferObjets;
using PRH.Client.Wasm.Services.Contracts;

namespace PRH.Client.Wasm.Services;

public class LookupService : ILookupService
{
    private readonly HttpClient _httpClient;

    public LookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<LookupDto>> GetModalidadesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/modalidades");
    }

    public async Task<IEnumerable<LookupDto>> GetTiposCampoAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-campo");
    }

    public async Task<LookupDto> GetTipoCampoByIdAsync(Guid tipoCampoId)
    {
        return await _httpClient.GetFromJsonAsync<LookupDto>($"api/lookups/tipos-campo/{tipoCampoId}");
    }

    public async Task<IEnumerable<LookupDto>> GetTiposFormularioAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-formulario");
    }

    public async Task<IEnumerable<LookupDto>> GetTiposVagaAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-vaga");
    }

    public async Task<IEnumerable<LookupDto>> GetEstadosAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/estados");
    }

    public async Task<IEnumerable<LookupDto>> GetResultadosAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/resultados");
    }

    /*public async Task<IEnumerable<LookupDto>> GetLookupByTipoAsync(string Tipo)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/" + Tipo);
    }*/

    /*public async Task<IEnumerable<LookupDto>> GetLookupsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups");
    }*/
}
