using PRH.Shared.DataTransferObjets.Entrevista;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Service.Contracts;

public interface IEntrevistaService
{
    Task<EntrevistaGetOneDto> GetEntrevistaById(Guid id, bool trackChanges);
    Task<Guid> GetEmpresaOfEntrevistaById(Guid id, bool trackChanges);
    Task<IEnumerable<EntrevistaGetAllDto>> GetEntrevistas(EntrevistaParameters param, bool trackChanges);
    Task<PagedListDto<EntrevistaGetAllDto>> GetEntrevistasPaginated(EntrevistaParameters param, bool trackChanges);
    Task<EntrevistaGetOneDto> CreateEntrevista(EntrevistaCreateDto dto, bool trackChanges);
    Task UpdateEntrevistaAsync(Guid entrevistaId, EntrevistaUpdateDto entrevistaNew, bool trackChanges);
}
