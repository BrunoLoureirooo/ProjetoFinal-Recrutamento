using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IRecrutamentoService
{
    Task<PagedListDto<RecrutamentoGetAllDto>> GetRecrutamentosPaginatedAsync(int page, int pageSize, string? nome, EmpresaDto empresa, DateTime? before, DateTime? after, bool? candDisponiveis);
    //Task<IEnumerable<RecrutamentoDto>> GetRecrutamentosAsync();
    Task<RecrutamentoGetOneDto> GetRecrutamentoByIdAsync(Guid id);
    Task<HttpResponseMessage> CreateRecrutamentoAsync(RecrutamentoCreateDto dto);
    Task UpdateRecrutamentoAsync(Guid id, RecrutamentoUpdateDto dto);
}
