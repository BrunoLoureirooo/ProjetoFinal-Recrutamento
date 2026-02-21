using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.RequestFeatures;
using PRH.Shared.DataTransferObjets.TipoDocumento;

namespace PRH.Server.Service.Contracts;

public interface IVagaService
{
    Task<VagaGetOneDto> GetVagaById(Guid id, bool trackChanges);
    Task<IEnumerable<VagaGetAllDto>> GetVagasByEmpresa(Guid empresaId, bool trackChanges);
    Task<PagedListDto<VagaGetAllDto>> GetVagasByEmpresaPaginated(Guid empresaId, VagaParameters param, bool trackChanges);
    Task<IEnumerable<TipoDocumentoGetVagaRequirementsDto>> GetRequiredFilesByVaga(Guid id, bool trackChanges);
    Task<IEnumerable<FormularioGetVagaFormsDto>> GetFormsByVaga(Guid id, bool trackChanges);
    Task<VagaGetOneDto> CreateVaga(VagaCreateDto dto, bool trackChanges);
    Task<bool> VagaBelongsToEmpresa(Guid vagaId, Guid empresaId, bool trackChanges);
    Task UpdateVagaAsync(Guid vagaId, VagaUpdateDto vagaNew, bool trackChanges);
}
