using System;
using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service
{
    public class FormularioCampoService : IFormularioCampoService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public FormularioCampoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FormularioCampoDto> CreateFormularioCampo(FormularioCampoDto dto, bool trackChanges)
		{
			if (dto == null) throw new ArgumentException("FormularioCampo vazio não permitido.");


			if (dto.FormularioId == null || dto.Campo == null) throw new ArgumentException("FormularioCampo deve conter um formulario e um campo.");

            var formulario = await _repository.Formulario.GetFormularioByIdAsync(dto.FormularioId, trackChanges);
            foreach (var formularioCampo in formulario.FormularioCampos)
            {
                if(formularioCampo.CampoId == dto.CampoId)
                {
                    throw new ArgumentException("O formulario já contem este campo.");
                }
            }

            Guid newFormularioCampoId = Guid.NewGuid();

			var FormularioCampo = new FormularioCampo
			{
				Id = newFormularioCampoId,
				CampoId = dto.CampoId,
				FormularioId = dto.FormularioId,
                Ativo = dto.Ativo
			};

			await _repository.FormularioCampo.CreateFormularioCampoAsync(FormularioCampo, trackChanges);

			var addedFormularioCampoDto = _mapper.Map<FormularioCampoDto>(FormularioCampo);

			return addedFormularioCampoDto;

        }


        public async Task<FormularioCampoDto> GetFormularioCampoByIdAsync(Guid formularioCampoId, bool trackChanges)
        {
            if (formularioCampoId == Guid.Empty)
                throw new ArgumentException("ID inválido para busca.");

            var formularioCampo = await _repository.FormularioCampo.GetFormularioCampoByIdAsync(formularioCampoId, trackChanges);

            if (formularioCampo == null)
                throw new InvalidOperationException("FormularioCampo não encontrado.");

            return _mapper.Map<FormularioCampoDto>(formularioCampo);
        }

        public async Task<FormularioCampoDto> AlterarStatusAtivo(Guid formularioCampoId, bool trackChanges)
        {
            if (formularioCampoId == Guid.Empty)
                throw new ArgumentException("ID inválido para busca.");

            // Buscar o campo de formulário
            var formularioCampo = await _repository.FormularioCampo.GetFormularioCampoByIdAsync(formularioCampoId, trackChanges);

            if (formularioCampo == null)
                throw new InvalidOperationException("FormularioCampo não encontrado.");

            // Atualizar o status do campo
            formularioCampo.Ativo = !formularioCampo.Ativo;

            // Atualizar o registro no repositório
            await _repository.FormularioCampo.UpdateFormularioCampoAsync(formularioCampo, trackChanges);

            // Mapear o objeto de volta para DTO
            return _mapper.Map<FormularioCampoDto>(formularioCampo);
        }



    }
}
