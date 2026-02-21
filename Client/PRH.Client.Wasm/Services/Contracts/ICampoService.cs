using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Campo;

namespace PRH.Client.Wasm.Services.Contracts;

public interface ICampoService
{
    Task<IEnumerable<CampoGetDto>> GetCamposAsync();
    Task<CampoGetDto> GetCampoByIdAsync(Guid id);
    Task<CampoGetDto> CreateCampoAsync(CampoCreateDto dto);
}
