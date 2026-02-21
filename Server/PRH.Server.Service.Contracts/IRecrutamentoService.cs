using PRH.Server.Entities.Models;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Service.Contracts;
public interface IRecrutamentoService
{
    Task<IEnumerable<RecrutamentoGetAllDto>> GetRecrutamentos(RecrutamentoParameters param, bool trackChanges);
    Task<RecrutamentoGetOneDto> GetRecrutamentoById(Guid id, bool trackChanges);
    Task<RecrutamentoAfterCreateDto> CreateRecrutamento(RecrutamentoCreateDto dto, bool trackChanges);
    Task<PagedListDto<RecrutamentoGetAllDto>> GetRecrutamentosPaginated(RecrutamentoParameters param, bool trackChanges);
    Task UpdateRecrutamento(Guid recrutamentoId, RecrutamentoUpdateDto recrutamentoNew, bool trackChanges);
}
