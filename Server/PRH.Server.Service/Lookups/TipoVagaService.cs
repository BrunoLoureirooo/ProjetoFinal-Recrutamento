using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Lookups;

public class TipoVagaService : ITipoVagaService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public TipoVagaService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LookupDto>> GetTiposVaga(bool trackChanges)
    {
        var tipoVagas = await _repository.TipoVaga.GetTiposVagaAsync(trackChanges);

        var tipoVagasDtos = _mapper.Map<IEnumerable<LookupDto>>(tipoVagas);

        return tipoVagasDtos;
    }
}
