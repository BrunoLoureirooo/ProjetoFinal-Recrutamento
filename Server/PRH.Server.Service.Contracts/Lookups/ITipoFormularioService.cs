using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts.Lookups;

public interface ITipoFormularioService
{
    Task<IEnumerable<LookupDto>> GetTiposFormulario(bool trackChanges);
}
