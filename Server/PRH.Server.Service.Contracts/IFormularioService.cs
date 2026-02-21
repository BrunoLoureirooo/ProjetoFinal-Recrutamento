using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Server.Service.Contracts;
public interface IFormularioService
{
	public Task<FormularioGetOneDto> CreateFormulario(FormularioCreateDto dto, bool trackChanges);
    public Task<IEnumerable<FormularioGetAllDto>> GetFormularios(bool trackChanges);
	public Task<FormularioGetOneDto> GetFormularioById(Guid id, bool trackChanges);
	public Task<PagedListDto<FormularioGetAllDto>> GetFormulariosPaginated(int page, int pageSize, bool trackChanges);
	public Task<FormularioGetOneDto> UpdateFormularioAsync(Guid id, FormularioUpdateDto dto, bool trackChanges);
}
