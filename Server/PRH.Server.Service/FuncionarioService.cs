using System;
using System.Collections.ObjectModel;
using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Funcionario;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Vaga;

namespace PRH.Server.Service
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public FuncionarioService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<FuncionarioGetOneDto> GetFuncionarioById(string id, bool trackChanges)
        {
            var funcionario = await _repository.Funcionario.GetFuncionarioByUserIdAsync(id, trackChanges);

            if (funcionario == null)
            {
                _logger.LogError("Funcionario com o id '" + id + "' não existe.");
                throw new ArgumentException("Funcionario com o id '" + id + "' não existe.");
            }

            var funcionarioGetAllDto = _mapper.Map<FuncionarioGetOneDto>(funcionario);

            return funcionarioGetAllDto;
        }

        public async Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionarios(bool trackChanges)
        {
            var funcionarios = await _repository.Funcionario.GetFuncionariosAsync(trackChanges);

            var funcionariosDto = _mapper.Map<IEnumerable<FuncionarioGetAllDto>>(funcionarios);

            return funcionariosDto;
        }

        public async Task<IEnumerable<FuncionarioGetAllDto>> GetFuncionariosByEmpresa(Guid empresaId, bool trackChanges)
        {
            if (!await _repository.Empresa.CheckIfEmpresaExists(empresaId, trackChanges))
            {
                _logger.LogError("Empresa com o id '" + empresaId + "' não existe.");
                throw new ArgumentException("Empresa com o id '" + empresaId + "' não existe.");
            }

            var funcs = await _repository.Funcionario.GetFuncionariosByEmpresaAsync(empresaId, trackChanges);

            var funcsDto = _mapper.Map<IEnumerable<FuncionarioGetAllDto>>(funcs);

            return funcsDto;
        }

        public async Task<PagedListDto<FuncionarioGetAllDto>> GetFuncionariosPaginated(int page, int pageSize, bool trackChanges)
        {
            var funcionarios = await _repository.Funcionario.GetFuncionariosPaginatedAsync(page, pageSize, trackChanges);

            var funcionariosDtos = _mapper.Map<IEnumerable<FuncionarioGetAllDto>>(funcionarios);
            var metaData = _mapper.Map<MetaDataDto>(funcionarios.MetaData);
            var response = new PagedListDto<FuncionarioGetAllDto> { Items = funcionariosDtos.ToList(), MetaData = metaData };

            return response;
        }
    }
}
