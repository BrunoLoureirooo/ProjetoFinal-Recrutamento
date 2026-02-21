using System.Security.Claims;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaDto>> GetEmpresas(bool trackChanges);
    Task<IEnumerable<EmpresaDto>> GetEmpresasByName(string name, bool trackChanges);
    Task<EmpresaDto> GetEmpresaByFuncionario(ClaimsPrincipal user, bool trackChanges);
    Task<EmpresaDto> GetEmpresaByGestor(ClaimsPrincipal user, bool trackChanges);
}
