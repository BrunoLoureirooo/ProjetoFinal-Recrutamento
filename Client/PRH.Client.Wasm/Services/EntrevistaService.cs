using System.Net.Http.Json;

using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Entrevista;

namespace PRH.Client.Wasm.Services;

public class EntrevistaService : IEntrevistaService
{
    private readonly HttpClient _httpClient;

    public EntrevistaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EntrevistaGetOneDto> GetEntrevistaByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<EntrevistaGetOneDto>("api/entrevistas/" + id);
    }
    public async Task<IEnumerable<EntrevistaGetAllDto>> GetEntrevistaByCandidaturaAsync(Guid CandidaturaId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>("api/entrevistas/candidatura/" + CandidaturaId);
    }

    public async Task<PagedListDto<EntrevistaGetAllDto>> GetEntrevistasPaginatedAsync(int page, int pageSize, string? nomeCandidato, Guid? resultado, DateTime? after, DateTime? before)
    {
        string endpoint = $"api/entrevistas?pagenumber={page}&pagesize={pageSize}";

        if (nomeCandidato is not null) endpoint += $"&nomecandidato={nomeCandidato}";
        if (resultado is not null) endpoint += $"&resultadoid={resultado}";
        if (before is not null) endpoint += $"&before={before.Value.ToString("yyyy-MM-ddTHH:mm:ss")}";
        if (after is not null) endpoint += $"&after={after.Value.ToString("yyyy-MM-ddTHH:mm:ss")}";

        return await _httpClient.GetFromJsonAsync<PagedListDto<EntrevistaGetAllDto>>(endpoint);
    }

    public async Task<IEnumerable<EntrevistaGetAllDto>> GetEntrevistasCalendarioAsync(DateTime after, DateTime before)
    {
        string endpoint = $"api/entrevistas?before={before.ToString("yyyy-MM-ddTHH:mm:ss")}&after={after.ToString("yyyy-MM-ddTHH:mm:ss")}";

        return await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>(endpoint);
    }

    public async Task<HttpResponseMessage> CreateEntrevistaAsync(EntrevistaCreateDto dto)
    {
        return await _httpClient.PostAsJsonAsync("api/entrevistas", dto);
    }
    
    public async Task UpdateEntrevistaAsync(Guid id, EntrevistaUpdateDto dto)
    {
        await _httpClient.PatchAsJsonAsync($"api/entrevistas/{id}", dto);
    }
}
