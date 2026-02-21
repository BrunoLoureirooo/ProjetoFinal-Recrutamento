using PRH.Server.Entities.Models;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository.Contracts;

public interface IFormularioRepository
{
	Task<IEnumerable<Formulario>> GetFormulariosAsync(bool trackChanges);
    Task<Formulario> GetFormularioByIdAsync(Guid id, bool trackChanges);
    Task<PagedList<Formulario>> GetFormulariosPaginatedAsync(int page, int pageSize, bool trackChanges);
    Task UpdateFormularioAsync(Formulario formulario);
    Task CreateFormularioAsync(Formulario Formulario, bool trackChanges);
}
