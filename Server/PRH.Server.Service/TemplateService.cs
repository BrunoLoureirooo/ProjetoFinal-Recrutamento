using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public TemplateService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper,
            IWebHostEnvironment env)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _env = env;
        }

        public async Task<TemplateDto> CreateTemplate(TemplateDto dto, bool trackChanges)
        {
            if (dto == null)
            {
                _logger.LogError("Template vazio não permitido.");
                throw new ArgumentException("Template vazio não permitido.");
            }

            if (!await CheckIfEmpresaExists(dto.EmpresaId, trackChanges))
            {
                _logger.LogError("Empresa com o id '" + dto.EmpresaId + "' não existe.");
                throw new ArgumentException("Empresa com o id '" + dto.EmpresaId + "' não existe.");
            }

            if (!await CheckIfTipoDocumentoExists(dto.TipoDocumentoId, trackChanges))
            {
                _logger.LogError("Empresa com o id '" + dto.TipoDocumentoId + "' n�o existe.");
                throw new ArgumentException("Empresa com o id '" + dto.TipoDocumentoId + "' n�o existe.");
            }

            Guid newTemplateId = Guid.NewGuid();
            var template = new Template
            {
                Id = newTemplateId,
                TipoDocumentoId = dto.TipoDocumentoId,
                Caminho = dto.Caminho,
                Preenchivel = dto.Preenchivel,
                EmpresaID = dto.EmpresaId
            };

            await _repository.Template.CreateTemplateAsync(template, trackChanges);

            var addedTemplateDto = _mapper.Map<TemplateDto>(template);

            return addedTemplateDto;
        }

        public async Task<IEnumerable<TemplateDto>> GetTemplates(bool trackChanges)
        {
            var templates = await _repository.Template.GetTemplatesAsync(trackChanges);

            var templatesDTO = _mapper.Map<IEnumerable<TemplateDto>>(templates);

            return templatesDTO;
        }

        public async Task<TemplateDto> GetTemplateById(Guid id, bool trackChanges)
        {
            var template = await _repository.Template.GetTemplateByIdAsync(id, trackChanges);

            if (template == null)
            {
                _logger.LogError("Template com o id '" + id + "' não existe.");
                throw new ArgumentException("Template com o id '" + id + "' não existe.");
            }


            var templateDto = _mapper.Map<TemplateDto>(template);

            return templateDto;
        }

        public async Task UpdateTemplateAsync(Guid id, TemplateDto dto)
        {
            var entity = await _repository.Template.GetTemplateByIdAsync(id, trackChanges: true);

            if (entity == null)
                throw new Exception("Template não encontrado");

            try
            {
                if (!string.IsNullOrEmpty(entity.Caminho))
                {
                    var filePath = Path.Combine(_env.ContentRootPath, entity.Caminho.TrimStart('/'));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar excluir o arquivo associado ao template: {ex.Message}");
                throw new Exception("Erro ao tentar excluir o arquivo associado ao template.", ex);
            }
            entity.EmpresaID = dto.EmpresaId;
            entity.TipoDocumentoId = dto.TipoDocumentoId;
            entity.Caminho = dto.Caminho;
            entity.Preenchivel = dto.Preenchivel;

            await _repository.Template.UpdateTemplateAsync(entity);
        }

        public async Task DeleteTemplateAsync(Guid id)
        {
            var entity = await _repository.Template.GetTemplateByIdAsync(id, trackChanges: false);
            if (entity == null)
                throw new ArgumentException($"Template com o id '{id}' não existe.");

            if (!string.IsNullOrEmpty(entity.Caminho))
            {
                var filePath = Path.Combine(_env.ContentRootPath, entity.Caminho.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarn($"Falha ao apagar o ficheiro {filePath}: {ex.Message}");
                    }
                }
            }

            await _repository.Template.DeleteTemplateAsync(id);
            await _repository.SaveAsync();
        }

        private async Task<bool> CheckIfEmpresaExists(Guid empresaId, bool empresaTrackChanges)
        {
            try
            {
                var empresa = await _repository.Empresa.GetEmpresaByIdAsync(empresaId, empresaTrackChanges);
                return empresa is not null;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> CheckIfTipoDocumentoExists(Guid tipoDocumentoId, bool trackChanges)
        {
            try
            {
                var tipoDocumento = await _repository.TipoDocumento.GetTipoDocumentoByIdAsync(tipoDocumentoId, trackChanges);
                return tipoDocumento is not null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TemplateDto>> GetTemplatesByEmpresaId(Guid empresaId, bool trackChanges)
        {
            if (!await CheckIfEmpresaExists(empresaId, trackChanges))
            {
                _logger.LogError("Empresa com o id '" + empresaId + "' não existe.");
                throw new ArgumentException("Empresa com o id '" + empresaId + "' não existe.");
            }

            var templates = await _repository.Template.GetTemplatesByEmpresaIdAsync(empresaId, trackChanges);

            var templatesDTO = _mapper.Map<IEnumerable<TemplateDto>>(templates);

            return templatesDTO;
        }

    }
}