using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.Contracts
{
    public interface IDocumentoService
    {

        Task<IEnumerable<DocumentoDto>> GetDocumentosByCandidaturaIdAsync(Guid candidaturaId);

        Task<DocumentoDto> GetDocumentoByIdAsync(Guid id);

        Task<DocumentoDto> CreateDocumentoFromTemplateAsync(Guid candidaturaId, Guid templateId, Dictionary<string, string>? extras = null);

        Task CreateDocumentoAsync(DocumentoDto dto);

        Task UpdateDocumentoAsync(Guid id, DocumentoDto dto);
        Task DeleteDocumentoAsync(Guid id);

        Task SignDocumentoDigitalAsync(Guid id);

        Task SignDocumentoFisicoAsync(Guid id);
    }
}
