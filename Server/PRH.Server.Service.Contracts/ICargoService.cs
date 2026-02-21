using PRH.Shared.DataTransferObjets.Cargo;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Service.Contracts;

public interface ICargoService
{
    Task<IEnumerable<CargoGetAllDto>> GetCargos(bool trackChanges);
    Task<PagedListDto<CargoGetAllDto>> GetCargosPaginated(CargoParameters param, bool trackChanges);
    Task<CargoGetOneDto> GetCargoByIdAsync(Guid id, bool trackChanges);
    Task<Guid> CreateCargo(CargoCreateDto dto, bool trackChanges);
}
