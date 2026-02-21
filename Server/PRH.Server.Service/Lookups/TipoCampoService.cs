using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Lookups;

public class TipoCampoService : ITipoCampoService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public TipoCampoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LookupDto>> GetTiposCampo(bool trackChanges)
    {
        var tipoCampos = await _repository.TipoCampo.GetTiposCampoAsync(trackChanges);

        var tipoCamposDtos = _mapper.Map<IEnumerable<LookupDto>>(tipoCampos);

        return tipoCamposDtos;
    }

    public async Task<LookupDto> GetTipoCampoById(Guid id, bool trackChanges)
    {
        var tipoCampo = await _repository.TipoCampo.GetTipoCampoByIdAsync(id, trackChanges);

        if (tipoCampo == null)
        {
            _logger.LogError("Tipo de campo com o id '" + id + "' não existe.");
            throw new ArgumentException("Tipo de campo com o id '" + id + "' não existe.");
        }


        var tipoCampoGetDto = _mapper.Map<LookupDto>(tipoCampo);

        return tipoCampoGetDto;
    }
}
