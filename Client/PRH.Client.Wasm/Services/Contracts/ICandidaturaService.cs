using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface ICandidaturaService
{
    Task<PagedListDto<CandidaturaGetAllDto>> GetCandidaturasPaginatedAsync(int page, int pageSize, string? recrutamentoNome, string? candidatoNome, EmpresaDto empresa, LookupDto estado);
    Task<CandidaturaGetOneDto> GetCandidaturaByIdAsync(Guid id);
    Task UpdateCandidaturaAsync(Guid id, CandidaturaUpdateDto dto);
}
