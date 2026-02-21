using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Vaga;
using System.Net.Http.Json;

namespace PRH.Client.Wasm.Services;

public class CandidaturaService : ICandidaturaService
{
    private readonly HttpClient _httpClient;
    public CandidaturaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CandidaturaGetOneDto> GetCandidaturaByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<CandidaturaGetOneDto>("api/candidaturas/" + id);
    }

    public async Task<PagedListDto<CandidaturaGetAllDto>> GetCandidaturasPaginatedAsync(int page, int pageSize, string? recrutamentoNome, string? candidatoNome, EmpresaDto empresa, LookupDto estado)
    {
        string endpoint = $"api/candidaturas?pagenumber={page}&pagesize={pageSize}";

        if (recrutamentoNome is not null) endpoint += $"&recrutamentonome={recrutamentoNome}";
        if (candidatoNome is not null) endpoint += $"&candidatonome={candidatoNome}";
        if (empresa is not null) endpoint += $"&empresaid={empresa.Id}";
        if (estado is not null) endpoint += $"&estadoid={estado.Id}";

        return await _httpClient.GetFromJsonAsync<PagedListDto<CandidaturaGetAllDto>>(endpoint);
    }

    public async Task UpdateCandidaturaAsync(Guid id, CandidaturaUpdateDto dto)
    {
        await _httpClient.PatchAsJsonAsync($"api/candidaturas/{id}", dto);
    }
}
