using System.Net.Http.Json;

using PRH.Shared.DataTransferObjets.Funcionario;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly HttpClient _httpClient;

    public FuncionarioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FuncionarioGetOneDto> GetFuncionarioByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<FuncionarioGetOneDto>("api/Funcionarios/" + id);
    }

    public async Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionariosAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<FuncionarioGetAllDto>>("api/Funcionarios");
    }

    public async Task<PagedListDto<FuncionarioGetAllDto>> GetFuncionariosPaginatedAsync(int page, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PagedListDto<FuncionarioGetAllDto>>($"api/Funcionarios?page={page}&pageSize={pageSize}");
    }

    public async Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionariosByEmpresaAsync(Guid empresaId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<FuncionarioGetAllDto>>($"api/funcionarios/empresa/{empresaId}");
    }

}
