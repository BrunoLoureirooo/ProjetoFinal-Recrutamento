using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts.Lookups;

public interface IResultadoService
{
    Task<IEnumerable<LookupDto>> GetResultados(bool trackChanges);
}
