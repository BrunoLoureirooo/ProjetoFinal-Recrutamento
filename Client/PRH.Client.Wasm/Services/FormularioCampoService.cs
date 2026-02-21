using System.Net.Http.Json;

using PRH.Shared.DataTransferObjets;
using PRH.Client.Wasm.Services.Contracts;

namespace PRH.Client.Wasm.Services;

public class FormularioCampoService : IFormularioCampoService
{
    private readonly HttpClient _httpClient;

    public FormularioCampoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task CreateFormularioCampoAsync(FormularioCampoDto dto)
    {
        await _httpClient.PostAsJsonAsync("api/FormularioCampos", dto);
    }

    public async Task ToggleFormularioCampo(Guid id, bool novoStatusAtivo)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/FormularioCampos/Toggle/{id}?ativo={novoStatusAtivo}", new { });

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Erro ao atualizar formulário: {msg}");
        }
    }

}
