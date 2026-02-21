using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface IFormularioCampoRepository
{
    Task CreateFormularioCampoAsync(FormularioCampo FormularioCampo, bool trackChanges);
    Task<FormularioCampo?> GetFormularioCampoByIdAsync(Guid id, bool trackChanges);
    Task UpdateFormularioCampoAsync(FormularioCampo formularioCampo, bool trackChanges);
}
