using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts.Lookups;

public interface IEstadoService
{
    Task<IEnumerable<LookupDto>> GetEstados(bool trackChanges);
}
