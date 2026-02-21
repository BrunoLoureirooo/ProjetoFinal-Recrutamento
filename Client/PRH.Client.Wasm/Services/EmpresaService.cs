using PRH.Client.Wasm.Pages;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets;
using System.Net.Http.Json;

namespace PRH.Client.Wasm.Services;

public class EmpresaService : IEmpresaService
{
    private readonly HttpClient _httpClient;

    public EmpresaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<EmpresaDto>> GetEmpresasAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<EmpresaDto>>("api/empresas");
    }

    public async Task<EmpresaDto> GetMyEmpresaAsync()
    {
        return await _httpClient.GetFromJsonAsync<EmpresaDto>("api/empresas/my");
    }

    public async Task<IEnumerable<EmpresaDto>> GetEmpresasByNameAsync(string nome)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<EmpresaDto>>("api/empresas?nome=" + nome);
    }
}
