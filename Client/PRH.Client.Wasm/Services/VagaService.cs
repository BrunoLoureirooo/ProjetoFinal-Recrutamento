using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.Vaga;
using System.Net.Http.Json;

namespace PRH.Client.Wasm.Services;
public class VagaService : IVagaService
{
    private readonly HttpClient _httpClient;

    public VagaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<VagaGetOneDto> GetVagaByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<VagaGetOneDto>("api/vagas/" + id);
    }

    public async Task CreateVagaAsync(VagaCreateDto dto)
    {
        await _httpClient.PostAsJsonAsync("api/vagas", dto);
    }

    public async Task UpdateVagaAsync(Guid id, VagaUpdateDto dto)
    {
        await _httpClient.PatchAsJsonAsync($"api/vagas/{id}", dto);
    }
}
