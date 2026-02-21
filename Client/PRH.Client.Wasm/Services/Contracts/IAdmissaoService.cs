using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IAdmissaoService
{
    Task<PagedListDto<AdmissaoGetAllDto>> GetAdmissoesPaginatedAsync(int page, int pageSize, string? nomeCandidato, string? nomeRecrutamento, EmpresaDto empresa);
}
