using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.Contracts
{
    public interface ITipoDocumentoService
    {
        Task CreateTipoDocumentoAsync(TipoDocumentoDto dto);
        Task<IEnumerable<TipoDocumentoDto>> GetTipoDocumentosAsync();
        Task<TipoDocumentoDto> GetTipoDocumentoByIdAsync(Guid id);
        
        Task UpdateTipoDocumentoAsync(Guid id, TipoDocumentoDto dto);
        Task DeleteTipoDocumentoAsync(Guid id);
    }
}
