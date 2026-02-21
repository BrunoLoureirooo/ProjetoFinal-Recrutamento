using System.Net.Http.Json;

using PRH.Shared.DataTransferObjets;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Recrutamento;

namespace PRH.Client.Wasm.Services;

public class RecrutamentoService : IRecrutamentoService
{
    private readonly HttpClient _httpClient;

    public RecrutamentoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RecrutamentoGetOneDto> GetRecrutamentoByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>("api/recrutamentos/" + id);
    }

    /*public async Task<IEnumerable<RecrutamentoDto>> GetRecrutamentosAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoDto>>("api/recrutamentos");
    }*/

    public async Task<PagedListDto<RecrutamentoGetAllDto>> GetRecrutamentosPaginatedAsync(int page, int pageSize, string? nome, EmpresaDto empresa, DateTime? before, DateTime? after, bool? candDisponiveis)
    {
        string endpoint = $"api/recrutamentos?pagenumber={page}&pagesize={pageSize}";

        if (nome is not null) endpoint += $"&nome={nome}";
        if (empresa is not null) endpoint += $"&empresaid={empresa.Id}";
        if (before is not null) endpoint += $"&before={before.Value.ToString("yyyy-MM-ddTHH:mm:ss")}";
        if (after is not null) endpoint += $"&after={after.Value.ToString("yyyy-MM-ddTHH:mm:ss")}";
        if (candDisponiveis is not null) endpoint += $"&candidaturasdisponiveis={candDisponiveis}";

        return await _httpClient.GetFromJsonAsync<PagedListDto<RecrutamentoGetAllDto>>(endpoint);
    }

    public async Task<HttpResponseMessage> CreateRecrutamentoAsync(RecrutamentoCreateDto dto)
    {
        return await _httpClient.PostAsJsonAsync("api/recrutamentos", dto);
    }

    public async Task UpdateRecrutamentoAsync(Guid id, RecrutamentoUpdateDto dto)
    {
        await _httpClient.PatchAsJsonAsync($"api/recrutamentos/{id}", dto);
    }
}
