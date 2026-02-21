using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Funcionario;
using PRH.Shared.DataTransferObjets.RequestFeatures;

namespace PRH.Server.Service.Contracts;
public interface IFuncionarioService
{
    public Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionarios(bool trackChanges);
    public Task<FuncionarioGetOneDto> GetFuncionarioById(string id, bool trackChanges);
    public Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionariosByEmpresa(Guid empresaId, bool trackChanges);
    public Task<PagedListDto<FuncionarioGetAllDto>> GetFuncionariosPaginated(int page, int pageSize, bool trackChanges);
}
