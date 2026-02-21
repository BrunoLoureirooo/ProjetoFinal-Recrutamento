using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts
{
    public interface IDocumentoRepository
    {
        Task<IEnumerable<Documento>> GetDocumentosByCandidaturaAsync(Guid candidaturaId, bool trackChanges);

        Task<Documento> GetDocumentoByIdAsync(Guid id, bool trackChanges);

        Task<Documento> GetDocumentoByCaminhoAsync(string caminho , bool trackChanges);

        Task<byte[]?> GetBytesByIdAsync(Guid id);

        Task CreateDocumentoAsync(Documento documento, bool trackChanges);

        Task UpdateDocumentoAsync(Documento documento);

        Task DeleteDocumentoAsync(Documento documento);

        Task SaveAsync();
    }
}
