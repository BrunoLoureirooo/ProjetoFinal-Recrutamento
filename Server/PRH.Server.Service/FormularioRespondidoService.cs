using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Formulario;

namespace PRH.Server.Service;

public class FormularioRespondidoService : IFormularioRespondidoService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public FormularioRespondidoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<IEnumerable<FormularioRespondidoDto>> GetRespostasByCandidatura(Guid candidaturaId, bool trackChanges)
    {
        if (!await _repository.Candidatura.CheckIfCandidaturaExists(candidaturaId, trackChanges))
        {
            _logger.LogError("Candidatura com o id '" + candidaturaId + "' não existe.");
            throw new ArgumentException("Candidatura com o id '" + candidaturaId + "' não existe.");
        }

        var respostas = await _repository.FormularioRespondido.GetRespostasByCandidaturaAsync(candidaturaId, trackChanges);

        var respostasDto = _mapper.Map<IEnumerable<FormularioRespondidoDto>>(respostas);

        return respostasDto;
    }
}
