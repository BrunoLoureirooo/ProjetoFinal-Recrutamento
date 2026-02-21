using PRH.Shared.DataTransferObjets.Cargo;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface ICargoService
{
    Task<IEnumerable<CargoGetAllDto>> GetCargosAsync();
    Task<PagedListDto<CargoGetAllDto>> GetCargosPaginatedAsync(int page, int pageSize);
    Task<CargoGetOneDto> GetCargoByIdAsync(Guid id);
    Task CreateCargoAsync(CargoCreateDto dto);
}
