using PRH.Server.Entities.Models;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts;
public interface IFormularioCampoService
{
    Task<FormularioCampoDto> CreateFormularioCampo(FormularioCampoDto dto, bool trackChanges);
    Task<FormularioCampoDto> GetFormularioCampoByIdAsync(Guid formularioCampoId, bool trackChanges);
    Task<FormularioCampoDto> AlterarStatusAtivo(Guid formularioCampoId, bool trackChanges);
}
