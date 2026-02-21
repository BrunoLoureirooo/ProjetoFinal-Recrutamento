using System;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts
{
    public interface ITipoDocumentoService
    {
        Task<TipoDocumentoDto> CreateTipoDocumentoAsync(TipoDocumentoDto dto, bool trackChanges);
        Task<IEnumerable<TipoDocumentoDto>> GetTipoDocumentosAsync(bool trackChanges);
        Task<TipoDocumentoDto> GetTipoDocumentoByIdAsync(Guid id, bool trackChanges);
        Task UpdateTipoDocumentoAsync(Guid id, TipoDocumentoDto dto);
        Task DeleteTipoDocumentoAsync(Guid id);
    }
}