using System;
using System.Security.Claims;
using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service;

public class EmpresaService : IEmpresaService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public EmpresaService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmpresaDto>> GetEmpresas(bool trackChanges)
    {
        var Empresas = await _repository.Empresa.GetEmpresasAsync(trackChanges);

        var EmpresasDto = _mapper.Map<IEnumerable<EmpresaDto>>(Empresas);

        return EmpresasDto;
    }

    public async Task<IEnumerable<EmpresaDto>> GetEmpresasByName(string name, bool trackChanges)
    {
        var Empresas = await _repository.Empresa.GetEmpresasByNameAsync(name, trackChanges);

        var EmpresasDto = _mapper.Map<IEnumerable<EmpresaDto>>(Empresas);

        return EmpresasDto;
    }

    public async Task<EmpresaDto> GetEmpresaById(Guid id, bool trackChanges)
    {
        var empresa = await _repository.Empresa.GetEmpresaByIdAsync(id, trackChanges);

        if (empresa == null)
        {
            _logger.LogError("Empresa com o id '" + id + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + id + "' não existe.");
        }


        var empresaDto = _mapper.Map<EmpresaDto>(empresa);

        return empresaDto;
    }

    public async Task<EmpresaDto> GetEmpresaByFuncionario(ClaimsPrincipal user, bool trackChanges)
    {
        var userId = user?.FindFirst("sub")?.Value ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("ID de usuário não encontrado");
            throw new ArgumentException("ID de usuário não encontrado");
        }

        var func = await _repository.Funcionario.GetFuncionarioByUserIdAsync(userId, trackChanges);
        if(func is null)
        {
            _logger.LogError("Funcionário com o ID de usuário '" + userId + "' não existe.");
            throw new ArgumentException("Funcionário com o ID de usuário '" + userId + "' não existe.");
        }

        return await GetEmpresaById(func.EmpresaId, trackChanges);
    }

    public async Task<EmpresaDto> GetEmpresaByGestor(ClaimsPrincipal user, bool trackChanges)
    {
        var userId = user?.FindFirst("sub")?.Value ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("ID de usuário não encontrado");
            throw new ArgumentException("ID de usuário não encontrado");
        }

        var gestor = await _repository.GestorCliente.GetGestorByUserIdAsync(userId, trackChanges);
        if (gestor is null)
        {
            _logger.LogError("Gestor com o ID de usuário '" + userId + "' não existe.");
            throw new ArgumentException("Gestor com o ID de usuário '" + userId + "' não existe.");
        }

        return await GetEmpresaById(gestor.EmpresaId, trackChanges);
    }
}
