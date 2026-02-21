using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts.Lookups;

public interface IModalidadeService
{
    Task<IEnumerable<LookupDto>> GetModalidades(bool trackChanges);
}
