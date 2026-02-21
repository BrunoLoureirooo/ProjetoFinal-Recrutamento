using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository
{
    internal sealed class TipoDocumentoRepository : RepositoryBase<TipoDocumento>, ITipoDocumentoRepository
    {
        public TipoDocumentoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateTipoDocumentoAsync(TipoDocumento tipoDocumento, bool trackChanges)
        {
            Create(tipoDocumento);
            await RepositoryContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TipoDocumento>> GetTipoDocumentosAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(td => td.Templates)
                .Include(td => td.Documentos)
                .Include(td => td.DocumentosNecessarios)
                .Include(td => td.EmpresaTipoDocumentos)
                .OrderBy(td => td.Nome)
                .ToListAsync();
        }

        public async Task<TipoDocumento> GetTipoDocumentoByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(td => td.Id.Equals(id), trackChanges)
                .Include(td => td.Templates)
                .Include(td => td.Documentos)
                .Include(td => td.DocumentosNecessarios)
                .Include(td => td.EmpresaTipoDocumentos)
                .FirstOrDefaultAsync();
        }

        public async Task<TipoDocumento> GetTipoDocumentoWithEmpresasAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(td => td.Id == id, trackChanges)
                    .Include(td => td.EmpresaTipoDocumentos)
                    .SingleOrDefaultAsync();
        }

        public async Task UpdateTipoDocumentoAsync(TipoDocumento tipoDocumento)
        {
            Update(tipoDocumento);
            await SaveAsync();
        }

        public async Task DeleteTipoDocumentoAsync(Guid id)
        {
            var entity = await GetTipoDocumentoByIdAsync(id, false);
            Delete(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }
}