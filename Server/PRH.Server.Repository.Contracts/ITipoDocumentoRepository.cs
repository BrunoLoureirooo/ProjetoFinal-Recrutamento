using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts
{
    public interface ITipoDocumentoRepository
    {
        Task CreateTipoDocumentoAsync(TipoDocumento tipoDocumento, bool trackChanges);
        Task<IEnumerable<TipoDocumento>> GetTipoDocumentosAsync(bool trackChanges);
        Task<TipoDocumento> GetTipoDocumentoByIdAsync(Guid id, bool trackChanges);
        Task<TipoDocumento> GetTipoDocumentoWithEmpresasAsync(Guid id, bool trackChanges);
        Task UpdateTipoDocumentoAsync(TipoDocumento tipoDocumento);
        Task DeleteTipoDocumentoAsync(Guid id);
        Task SaveAsync();
    }
}
