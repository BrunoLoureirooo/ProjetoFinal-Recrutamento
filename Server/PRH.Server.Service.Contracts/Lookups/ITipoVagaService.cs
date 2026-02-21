using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts.Lookups;

public interface ITipoVagaService
{
    Task<IEnumerable<LookupDto>> GetTiposVaga(bool trackChanges);
}
