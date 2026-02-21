using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Lookups;

public class TipoFormularioService : ITipoFormularioService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public TipoFormularioService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LookupDto>> GetTiposFormulario(bool trackChanges)
    {
        var tipoFormularios = await _repository.TipoFormulario.GetTiposFormularioAsync(trackChanges);

        var tipoFormulariosDtos = _mapper.Map<IEnumerable<LookupDto>>(tipoFormularios);

        return tipoFormulariosDtos;
    }
}
