using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TipoDocumentoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TipoDocumentoDto> CreateTipoDocumentoAsync(TipoDocumentoDto dto, bool trackChanges)
        {
            if (dto == null)
                throw new ArgumentException("TipoDocumento vazio não permitido.");

            var id = Guid.NewGuid();
            var entity = new TipoDocumento
            {
                Id = id,
                Nome = dto.Nome,
                Assinavel = dto.Assinavel,
                EmpresaTipoDocumentos = new Collection<EmpresaTipoDocumento>(
                    dto.EmpresaTipoDocumentos
                       .Select(x => new EmpresaTipoDocumento
                       {
                           EmpresaId = x.EmpresaId,
                           TipoDocumentoId = id
                       })
                       .ToList()
                )
            };

            await _repository.TipoDocumento.CreateTipoDocumentoAsync(entity, trackChanges);
            await _repository.SaveAsync();

            return _mapper.Map<TipoDocumentoDto>(entity);
        }

        public async Task<IEnumerable<TipoDocumentoDto>> GetTipoDocumentosAsync(bool trackChanges)
        {
            var entities = await _repository.TipoDocumento.GetTipoDocumentosAsync(trackChanges);
            return _mapper.Map<IEnumerable<TipoDocumentoDto>>(entities);
        }

        public async Task<TipoDocumentoDto> GetTipoDocumentoByIdAsync(Guid id, bool trackChanges)
        {
            var entity = await _repository.TipoDocumento.GetTipoDocumentoByIdAsync(id, trackChanges);
            if (entity == null)
            {
                _logger.LogError($"TipoDocumento com o id '{id}' não existe.");
                throw new ArgumentException($"TipoDocumento com o id '{id}' não existe.");
            }
            return _mapper.Map<TipoDocumentoDto>(entity);
        }

        public async Task UpdateTipoDocumentoAsync(Guid id, TipoDocumentoDto dto)
        {
            if (dto == null)
                throw new ArgumentException("TipoDocumento para atualização não pode ser nulo.");

            // busca já com a lista de EmpresaTipoDocumentos
            var entity = await _repository.TipoDocumento
                              .GetTipoDocumentoWithEmpresasAsync(id, trackChanges: true);

            if (entity == null)
                throw new ArgumentException($"TipoDocumento com o id '{id}' não existe.");

            entity.Nome = dto.Nome;
            entity.Assinavel = dto.Assinavel;

            // remove associações desmarcadas
            var toRemove = entity.EmpresaTipoDocumentos
                .Where(etd => !dto.EmpresaTipoDocumentos.Any(x => x.EmpresaId == etd.EmpresaId))
                .ToList();
            foreach (var etd in toRemove)
                entity.EmpresaTipoDocumentos.Remove(etd);

            // adiciona novas associações
            var existingEmpresaIds = entity.EmpresaTipoDocumentos.Select(etd => etd.EmpresaId).ToHashSet();
            foreach (var x in dto.EmpresaTipoDocumentos)
            {
                if (!existingEmpresaIds.Contains(x.EmpresaId))
                    entity.EmpresaTipoDocumentos.Add(new EmpresaTipoDocumento
                    {
                        EmpresaId = x.EmpresaId,
                        TipoDocumentoId = id
                    });
            }

            await _repository.SaveAsync();
        }

        public async Task DeleteTipoDocumentoAsync(Guid id)
        {
            var entity = await _repository.TipoDocumento.GetTipoDocumentoByIdAsync(id, trackChanges: false);
            if (entity == null)
            {
                _logger.LogError($"TipoDocumento com o id '{id}' não encontrado para exclusão.");
                throw new ArgumentException($"TipoDocumento com o id '{id}' não existe.");
            }

            await _repository.TipoDocumento.DeleteTipoDocumentoAsync(id);
            await _repository.SaveAsync();
        }
    }
}
