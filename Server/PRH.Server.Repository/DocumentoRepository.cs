using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository
{
    public class DocumentoRepository : RepositoryBase<Documento>, IDocumentoRepository
    {
        private readonly RepositoryContext _context;

        public DocumentoRepository(RepositoryContext context)
            : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Documento>> GetDocumentosByCandidaturaAsync(Guid candidaturaId, bool trackChanges)
        {
            return await FindByCondition(d => d.CandidaturaId == candidaturaId, trackChanges)
                .Include(d => d.Template)
                .Include(d => d.TipoDocumento)
                .ToListAsync();
        }

        public async Task<Documento> GetDocumentoByIdAsync(Guid id, bool trackChanges)
        {
            var doc = await FindByCondition(d => d.Id == id, trackChanges)
                .Include(d => d.Template)
                .Include(d => d.TipoDocumento)
                .SingleOrDefaultAsync();

            if (doc == null)
                throw new ArgumentException($"Documento com o id '{id}' não encontrado.");

            return doc;
        }

        public async Task CreateDocumentoAsync(Documento documento, bool trackChanges)
        {
            Create(documento);
            await RepositoryContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentoAsync(Documento documento)
        {
            Delete(documento);
            await SaveAsync();
        }

        public async Task UpdateDocumentoAsync(Documento documento)
        {
            Update(documento);
            await SaveAsync();
        }

        public async Task<Documento> GetDocumentoByCaminhoAsync(string caminho, bool trackChanges)
        {
            var doc = await FindByCondition(d => d.Caminho == caminho, trackChanges)
                 .SingleOrDefaultAsync();

            if (doc == null)
                throw new ArgumentException($"Documento com o caminho '{caminho}' não encontrado.");

            return doc;

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]?> GetBytesByIdAsync(Guid id)
        {
            var documento = this.GetDocumentoByIdAsync(id, false).Result;

            if (documento == null)
                return null;

            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), documento.Caminho.TrimStart('/', '\\'));

            if (!File.Exists(caminhoFisico))
                return null;

            return await File.ReadAllBytesAsync(caminhoFisico);
        }
    }
}
