using PRH.Shared.DataTransferObjets.Entrevista;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IEntrevistaService
{
    Task<EntrevistaGetOneDto> GetEntrevistaByIdAsync(Guid id);
    Task<PagedListDto<EntrevistaGetAllDto>> GetEntrevistasPaginatedAsync(int page, int pageSize, string? nomeCandidato, Guid? resultado, DateTime? after, DateTime? before);
    Task<IEnumerable<EntrevistaGetAllDto>> GetEntrevistasCalendarioAsync(DateTime after, DateTime before);
    Task<HttpResponseMessage> CreateEntrevistaAsync(EntrevistaCreateDto dto);
    Task<IEnumerable<EntrevistaGetAllDto>> GetEntrevistaByCandidaturaAsync(Guid CandidaturaId);
    Task UpdateEntrevistaAsync(Guid id, EntrevistaUpdateDto dto);
}
