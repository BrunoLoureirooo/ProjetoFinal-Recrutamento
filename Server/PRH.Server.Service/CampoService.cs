using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Campo;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Bogus.DataSets;


namespace PRH.Server.Service
{
    public class CampoService : ICampoService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CampoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampoGetDto>> GetCampos(bool trackChanges)
        {
            var campos = await _repository.Campo.GetCamposAsync(trackChanges);
            return _mapper.Map<IEnumerable<CampoGetDto>>(campos);
        }

        public async Task<CampoGetDto> CreateCampoAsync(CampoCreateDto dto, bool trackChanges)
        {
            if (dto == null)
                throw new ArgumentException("Campo vazio não permitido.");

            // Busca a entidade TipoCampo completa
            var tipoCampo = await GetTipoCampoById(dto.TipoCampo.Id, trackChanges);

            // Geração do nome único
            var nomeUnicoFinal = await GerarNomeUnicoValido(dto.Nome);

            var novoCampo = new Campo
            {
                Id = Guid.NewGuid(),
                TipoId = tipoCampo.Id,         // Entidade completa
                Nome = dto.Nome,
                NomeUnico = nomeUnicoFinal,
                IsOpcional = dto.IsOpcional
            };

            await _repository.Campo.CreateCampoAsync(novoCampo, trackChanges);
            var campo =  _mapper.Map<CampoGetDto>(novoCampo);
            return campo;
        }

        private async Task<string> GerarNomeUnicoValido(string nomeBase)
        {
            var nomeUnicoBase = RemoverAcentos(nomeBase.ToUpper()).Replace(" ", "_");

            var nomeUnicoFinal = nomeUnicoBase;
            int sufixo = 1;

            var nomeUnicosExistentes = await _repository.Campo.GetCamposByNomeUnico(nomeUnicoBase);

            while (nomeUnicosExistentes.Contains(nomeUnicoFinal))
            {
                nomeUnicoFinal = $"{nomeUnicoBase}_{sufixo}";
                sufixo++;
            }

            return nomeUnicoFinal;
        }

        private async Task<TipoCampo> GetTipoCampoById(Guid tipoId, bool trackChanges)
        {
            var tipo = await _repository.TipoCampo.GetTipoCampoByIdAsync(tipoId, trackChanges);

            if (tipo == null)
            {
                _logger.LogError($"TipoCampo com ID {tipoId} não encontrado.");
                throw new ArgumentException($"TipoCampo com ID {tipoId} não existe.");
            }

            return tipo;
        }

        public async Task<IEnumerable<CampoGetDto>> GetCamposByVaga(Guid vagaId, bool trackChanges)
        {
            if (!await _repository.Vaga.CheckIfVagaExists(vagaId, trackChanges))
            {
                _logger.LogError("Vaga com o id '" + vagaId + "' não existe.");
                throw new ArgumentException("Vaga com o id '" + vagaId + "' não existe.");
            }

            var campos = await _repository.Campo.GetCamposByVagaAsync(vagaId, trackChanges);

            var camposDto = _mapper.Map<IEnumerable<CampoGetDto>>(campos);

            return camposDto;
        }

        public string RemoverAcentos(string texto)
        {
            var normalizedString = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}