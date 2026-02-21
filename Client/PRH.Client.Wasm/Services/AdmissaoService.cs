using System.Net.Http.Json;

using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services;

public class AdmissaoService : IAdmissaoService
{
    private readonly HttpClient _httpClient;

    public AdmissaoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<PagedListDto<AdmissaoGetAllDto>> GetAdmissoesPaginatedAsync(int page, int pageSize, string? nomeCandidato, string? nomeRecrutamento, EmpresaDto empresa)
    {
        string endpoint = $"api/admissoes?pagenumber={page}&pagesize={pageSize}";

        if (nomeCandidato is not null) endpoint += $"&candidatonome={nomeCandidato}";
        if (nomeRecrutamento is not null) endpoint += $"&recrutamentonome={nomeRecrutamento}";
        if (empresa is not null) endpoint += $"&empresaid={empresa.Id}";

        return await _httpClient.GetFromJsonAsync<PagedListDto<AdmissaoGetAllDto>>(endpoint);
    }


}
