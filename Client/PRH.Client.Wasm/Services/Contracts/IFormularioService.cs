using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IFormularioService
{
    Task<FormularioGetOneDto> GetFormularioByIdAsync(Guid id);
    Task<IEnumerable<FormularioGetAllDto>> GetFormulariosAsync();
    Task<PagedListDto<FormularioGetAllDto>> GetFormulariosPaginatedAsync(int page, int pageSize);
    Task UpdateFormularioAsync(Guid id, FormularioUpdateDto dto);
    Task<FormularioGetOneDto> CreateFormularioAsync(FormularioCreateDto dto);

}
