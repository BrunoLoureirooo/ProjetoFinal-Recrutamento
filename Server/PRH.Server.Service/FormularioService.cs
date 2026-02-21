using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Bogus.DataSets;

namespace PRH.Server.Service
{
	public class FormularioService : IFormularioService
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		public FormularioService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
		{
			_repository = repositoryManager;
			_logger = logger;
			_mapper = mapper;
		}

        public async Task<FormularioGetOneDto> CreateFormulario(FormularioCreateDto dto, bool trackChanges)
		{
			if (dto == null) throw new ArgumentException("Formulario vazio não permitido.");


			if (dto.FormularioCampos == null || dto.FormularioCampos.Count == 0) throw new ArgumentException("Formulario deve conter Campos.");

			Guid newFormularioId = Guid.NewGuid();
            var todosFormularios = await _repository.Formulario.GetFormulariosAsync(trackChanges);
            var nomesUnicosExistentes = todosFormularios.Select(f => f.NomeUnico).ToList();


            var baseNomeUnico = RemoverAcentos(dto.Nome.ToUpper()).Replace(" ", "_");
            string nomeUnico = baseNomeUnico;
            int contador = 1;

            // Verifica se algum nome existente contém o nome base e incrementa até achar um disponível
            while (nomesUnicosExistentes.Any(n => n.Contains(nomeUnico)))
            {
                nomeUnico = $"{baseNomeUnico}_{contador}";
                contador++;
            }

            var Formulario = new Formulario
			{
				Id = newFormularioId,
				Nome = dto.Nome,
				NomeUnico = nomeUnico,
				TipoId = dto.TipoFormulario.Id,
				FormularioCampos = new Collection<FormularioCampo>(dto.FormularioCampos.Select(fc => new FormularioCampo 
				{
					Id = Guid.NewGuid(),
					FormularioId = newFormularioId,
                    CampoId = fc.CampoId,
                    Ativo = fc.Ativo
				}).ToList())
			};
			_repository.Formulario.CreateFormularioAsync(Formulario, trackChanges);

			var addedFormularioGetAllDto = _mapper.Map<FormularioGetOneDto>(Formulario);


			return addedFormularioGetAllDto;
		}
        public async Task<FormularioGetOneDto> UpdateFormularioAsync(Guid id, FormularioUpdateDto dto, bool trackChanges)
        {
            // Obter o formulário
            var formulario = await _repository.Formulario.GetFormularioByIdAsync(id, trackChanges);

            // Verificar se o formulário existe
            if (formulario is null)
            {
                _logger.LogError($"Formulário com id '{id}' não encontrado.");
                throw new ArgumentException($"Formulário com id '{id}' não encontrado.");
            }

            // Atualizar o TipoId (associando o tipo)
            formulario.TipoId = dto.TipoFormulario.Id;

            // Mapear as outras propriedades do DTO para a entidade formulário
            _mapper.Map(dto, formulario);

            // Atualizar no repositório
            await _repository.Formulario.UpdateFormularioAsync(formulario);
            await _repository.SaveAsync();

            // Obter novamente a entidade para garantir que as alterações foram aplicadas corretamente
            var updatedEntity = await _repository.Formulario.GetFormularioByIdAsync(id, trackChanges: false);

            // Mapear a entidade atualizada para o DTO e retornar
            return _mapper.Map<FormularioGetOneDto>(updatedEntity);
        }




        public async Task<FormularioGetOneDto> GetFormularioById(Guid id, bool trackChanges)
		{
			var formulario = await _repository.Formulario.GetFormularioByIdAsync(id, trackChanges);

            if (formulario == null)
            {
                _logger.LogError("Formulário com o id '" + id + "' não existe.");
                throw new ArgumentException("Formulário com o id '" + id + "' não existe.");
            }

            var FormularioGetAllDto = _mapper.Map<FormularioGetOneDto>(formulario);

			return FormularioGetAllDto;
        }

		public async Task<IEnumerable<FormularioGetAllDto>> GetFormularios(bool trackChanges)
		{
			var formularios = await _repository.Formulario.GetFormulariosAsync(trackChanges);

			var formulariosDto = _mapper.Map<IEnumerable<FormularioGetAllDto>>(formularios);

			return formulariosDto;
		}

        public async Task<PagedListDto<FormularioGetAllDto>> GetFormulariosPaginated(int page, int pageSize, bool trackChanges)
        {
            var formularios = await _repository.Formulario.GetFormulariosPaginatedAsync(page, pageSize, trackChanges);

            var formulariosDtos = _mapper.Map<IEnumerable<FormularioGetAllDto>>(formularios);
            var metaData = _mapper.Map<MetaDataDto>(formularios.MetaData);
            var response = new PagedListDto<FormularioGetAllDto> { Items = formulariosDtos.ToList(), MetaData = metaData };

            return response;
        }

        string RemoverAcentos(string texto)
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
