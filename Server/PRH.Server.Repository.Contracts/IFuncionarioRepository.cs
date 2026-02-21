using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface IFuncionarioRepository
{
    Task<Funcionario> GetFuncionarioByUserIdAsync(string userId, bool trackChanges);
    Task<Funcionario> GetFuncionarioByIdAsync(Guid Id, bool trackChanges);
    Task<PagedList<Funcionario>> GetFuncionariosPaginatedAsync(int page, int pageSize, bool trackChanges);
    Task<IEnumerable<Funcionario>> GetFuncionariosAsync(bool trackChanges);
    Task<IEnumerable<Funcionario>> GetFuncionariosByEmpresaAsync(Guid empresaId, bool trackChanges);
}
