using PRH.Shared.DataTransferObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Service.Contracts.Lookups;

public interface ITipoCampoService
{
    Task<IEnumerable<LookupDto>> GetTiposCampo(bool trackChanges);
    Task<LookupDto> GetTipoCampoById(Guid id, bool trackChanges);
}
