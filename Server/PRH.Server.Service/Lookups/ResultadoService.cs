using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Lookups;
public class ResultadoService : IResultadoService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public ResultadoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LookupDto>> GetResultados(bool trackChanges)
    {
        var estados = await _repository.Resultado.GetResultadosAsync(trackChanges);

        var estadosDtos = _mapper.Map<IEnumerable<LookupDto>>(estados);

        return estadosDtos;
    }
}
