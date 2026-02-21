using Microsoft.AspNetCore.Http;
using PRH.Server.Entities.Models;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Service.Contracts;
public interface ICandidaturaService
{
    Task<IEnumerable<CandidaturaGetAllDto>> GetCandidaturas(CandidaturaParameters param, bool trackChanges);
    Task<CandidaturaGetOneDto> GetCandidaturaById(Guid id, bool trackChanges);
    Task<CandidaturaGetOneDto> CreateCandidatura(CandidaturaCreateDto dto, bool trackChanges);
    Task<PagedListDto<CandidaturaGetAllDto>> GetCandidaturasPaginated(CandidaturaParameters param, bool trackChanges); 
    Task UpdateCandidaturaAsync(Guid candidaturaId, CandidaturaUpdateDto candidaturaNew, bool trackChanges);
    Task<IEnumerable<CandidaturaGetAllDto>> GetCandidaturasByEmpresa(Guid empresaId, CandidaturaParameters param, bool trackChanges);
    Task<PagedListDto<CandidaturaGetAllDto>> GetCandidaturasByEmpresaPaginated(Guid empresaId, CandidaturaParameters param, bool trackChanges);
    Task<IEnumerable<AdmissaoGetAllDto>> GetAdmissao(AdmissaoParameters param, bool trackChanges);
    Task<PagedListDto<AdmissaoGetAllDto>> GetAdmissaoPaginated(AdmissaoParameters param, bool trackChanges);

}
