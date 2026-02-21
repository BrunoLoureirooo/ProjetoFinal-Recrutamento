using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Lookups;
public class EstadoService : IEstadoService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public EstadoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LookupDto>> GetEstados(bool trackChanges)
    {
        var estados = await _repository.Estado.GetEstadosAsync(trackChanges);

        var estadosDtos = _mapper.Map<IEnumerable<LookupDto>>(estados);

        return estadosDtos;
    }
}
