using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Lookups;

public class ModalidadeService : IModalidadeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public ModalidadeService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LookupDto>> GetModalidades(bool trackChanges)
    {
        var modalidades = await _repository.Modalidade.GetModalidadesAsync(trackChanges);

        var modalidadesDtos = _mapper.Map<IEnumerable<LookupDto>>(modalidades);

        return modalidadesDtos;
    }
}
