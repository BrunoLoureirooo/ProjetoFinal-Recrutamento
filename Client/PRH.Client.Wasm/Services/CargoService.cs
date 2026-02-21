using System.Net.Http.Json;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Cargo;

namespace PRH.Client.Wasm.Services;

public class CargoService : ICargoService
{
    private readonly HttpClient _httpClient;

    public CargoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CargoGetAllDto>> GetCargosAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<CargoGetAllDto>>("api/cargos");
    }
    public async Task<CargoGetOneDto> GetCargoByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<CargoGetOneDto>($"api/cargos/{id}");
    }

    public async Task<PagedListDto<CargoGetAllDto>> GetCargosPaginatedAsync(int page, int pageSize)
    {
        string endpoint = $"api/cargos?pagenumber={page}&pagesize={pageSize}";
        return await _httpClient.GetFromJsonAsync<PagedListDto<CargoGetAllDto>>(endpoint);
    }

    public async Task CreateCargoAsync(CargoCreateDto dto)
    {
        await _httpClient.PostAsJsonAsync("api/cargos", dto);
    }
}
