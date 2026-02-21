using PRH.Shared.DataTransferObjets.Funcionario;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IFuncionarioService
{
    Task<FuncionarioGetOneDto> GetFuncionarioByIdAsync(string id);
    Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionariosAsync();
    Task<PagedListDto<FuncionarioGetAllDto>> GetFuncionariosPaginatedAsync(int page, int pageSize);
    Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionariosByEmpresaAsync(Guid empresaId);
}
