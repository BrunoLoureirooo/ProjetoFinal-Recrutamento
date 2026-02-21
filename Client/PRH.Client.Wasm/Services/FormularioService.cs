using System.Net.Http.Json;

using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services;

public class FormularioService : IFormularioService
{
	private readonly HttpClient _httpClient;

	public FormularioService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<FormularioGetOneDto> GetFormularioByIdAsync(Guid id)
	{
		return await _httpClient.GetFromJsonAsync<FormularioGetOneDto>("api/formularios/" + id);
	}
	
	public async Task<IEnumerable<FormularioGetAllDto>> GetFormulariosAsync()
	{
		return await _httpClient.GetFromJsonAsync<IEnumerable<FormularioGetAllDto>>("api/formularios");
	}

    public async Task<PagedListDto<FormularioGetAllDto>> GetFormulariosPaginatedAsync(int page, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PagedListDto<FormularioGetAllDto>>($"api/formularios?page={page}&pageSize={pageSize}");
    }

    public async Task UpdateFormularioAsync(Guid id, FormularioUpdateDto dto)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/formularios/{id}", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Erro ao atualizar formulário: {error}");
        }
    }

    public async Task<FormularioGetOneDto> CreateFormularioAsync(FormularioCreateDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/formularios", dto);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<FormularioGetOneDto>();
    }


}
