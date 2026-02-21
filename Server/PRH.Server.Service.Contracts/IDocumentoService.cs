using System;
using PRH.Shared.DataTransferObjets;
using Microsoft.AspNetCore.Http;

namespace PRH.Server.Service.Contracts
{
    public interface IDocumentoService
    {
        Task<IEnumerable<DocumentoDto>> GetDocumentosByCandidaturaAsync(Guid candidaturaId);

        Task<DocumentoDto> GetDocumentoByIdAsync(Guid id);

        Task<DocumentoDto> UploadDocumentoAsync(Guid candidaturaId, IFormFile file, Guid tipoDocumentoId);

        Task<DocumentoDto> CreateDocumentoAsync(DocumentoDto dto, bool trackChanges);

        Task<byte[]?> ObterBytesDocumentoAsync(Guid id);


        Task UpdateDocumentoAsync(Guid id, DocumentoDto dto);

        Task AssinarDocumentoAsync(AssinaturaDto dto, bool trackChanges);

        Task<DocumentoDto> CreateDocumentoFromTemplateAsync(Guid candidaturaId, Guid templateId, Dictionary<string,string>? extras = null);

        Task DeleteDocumentoAsync(Guid id);

    }
}
