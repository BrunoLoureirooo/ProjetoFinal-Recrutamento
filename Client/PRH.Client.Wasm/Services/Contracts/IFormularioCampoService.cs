using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IFormularioCampoService
{
    Task CreateFormularioCampoAsync(FormularioCampoDto dto);
    Task ToggleFormularioCampo(Guid id, bool novoStatusAtivo);
}
