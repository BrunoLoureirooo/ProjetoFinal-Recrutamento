using System;
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
        var empresas = await _repository.Empresa.GetEmpresasAsync(trackChanges);

        var empresasDto = _mapper.Map<IEnumerable<EmpresaDto>>(empresas);

        return empresasDto;
    }
    
}
