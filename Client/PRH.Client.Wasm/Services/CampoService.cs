using System.Net.Http.Json;

using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.Campo;

namespace PRH.Client.Wasm.Services;

public class CampoService : ICampoService
{
    private readonly HttpClient _httpClient;

    public CampoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CampoGetDto>> GetCamposAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<CampoGetDto>>("api/Campos");
    }

    public async Task<CampoGetDto> GetCampoByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<CampoGetDto>("api/campo/" + id);
    }

    public async Task<CampoGetDto> CreateCampoAsync(CampoCreateDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Campos", dto);

        if (response.IsSuccessStatusCode)
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CampoGetDto>();
        }
        else
        {
            // Optionally: log or handle errors here
            var error = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Erro ao criar campo: {error}");
        }
    }

}
