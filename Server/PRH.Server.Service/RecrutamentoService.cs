using System.Collections.ObjectModel;
using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Service;

public class RecrutamentoService : IRecrutamentoService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IVagaService _vagaService;
    public RecrutamentoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IVagaService vagaService)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
        _vagaService = vagaService;
    }

    public async Task<RecrutamentoAfterCreateDto> CreateRecrutamento(RecrutamentoCreateDto dto, bool trackChanges)
    {
        if (dto == null)
        {
            _logger.LogError("Recrutamento vazio não permitido.");
            throw new ArgumentException("Recrutamento vazio não permitido.");
        }

        if(!await _repository.Empresa.CheckIfEmpresaExists((Guid)dto.EmpresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + dto.EmpresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + dto.EmpresaId + "' não existe.");
        }

        Guid newRecrutamentoId = Guid.NewGuid();

        Recrutamento recrutamento = new()
        {
            Id = newRecrutamentoId,
            Nome = dto.Nome,
            EmpresaID = (Guid)dto.EmpresaId,
            Data = DateTime.Now
        };

        await _repository.Recrutamento.CreateRecrutamentoAsync(recrutamento, trackChanges);

        foreach(VagaCreateRecrutamentoDto v in dto.Vagas)
        {
            VagaCreateDto vagaDto = _mapper.Map<VagaCreateDto>(v);
            vagaDto.RecrutamentoId = newRecrutamentoId;

            await _vagaService.CreateVaga(vagaDto, trackChanges);
        }

        RecrutamentoAfterCreateDto recrutamentoDto = _mapper.Map<RecrutamentoAfterCreateDto>(recrutamento);
        return recrutamentoDto;
    }

    public async Task<RecrutamentoGetOneDto> GetRecrutamentoById(Guid id, bool trackChanges)
    {
        var recrutamento = await _repository.Recrutamento.GetRecrutamentoByIdAsync(id, trackChanges);

        if(recrutamento == null)
        {
            _logger.LogError("Recrutamento com o id '" + id + "' não existe.");
            throw new ArgumentException("Recrutamento com o id '" + id + "' não existe.");
        }


        var recrutamentoDto = _mapper.Map<RecrutamentoGetOneDto>(recrutamento);

        return recrutamentoDto;
    }

    public async Task<IEnumerable<RecrutamentoGetAllDto>> GetRecrutamentos(RecrutamentoParameters param, bool trackChanges)
    {
        if (param.After is not null && param.Before is not null && !param.ValidDates)
        {
            _logger.LogError("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
            throw new ArgumentException("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
        }

        if (param.EmpresaId is not null && !await _repository.Empresa.CheckIfEmpresaExists((Guid)param.EmpresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + (Guid)param.EmpresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + (Guid)param.EmpresaId + "' não existe.");
        }

        var recrutamentos = await _repository.Recrutamento.GetRecrutamentosAsync(param, trackChanges);

        var recrutamentosDto = _mapper.Map<IEnumerable<RecrutamentoGetAllDto>>(recrutamentos);

        return recrutamentosDto;
    }

    public async Task<PagedListDto<RecrutamentoGetAllDto>> GetRecrutamentosPaginated(RecrutamentoParameters param, bool trackChanges)
    {
        if (param.After is not null && param.Before is not null && !param.ValidDates)
        {
            _logger.LogError("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
            throw new ArgumentException("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
        }

        if (param.EmpresaId is not null && !await _repository.Empresa.CheckIfEmpresaExists((Guid)param.EmpresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + (Guid)param.EmpresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + (Guid)param.EmpresaId + "' não existe.");
        }

        var recrutamentos = await _repository.Recrutamento.GetRecrutamentosPaginatedAsync(param, trackChanges);

        var recrutamentosDto = _mapper.Map<IEnumerable<RecrutamentoGetAllDto>>(recrutamentos);
        var metaData = _mapper.Map<MetaDataDto>(recrutamentos.MetaData);
        var response = new PagedListDto<RecrutamentoGetAllDto> { Items = recrutamentosDto.ToList(), MetaData = metaData};

        return response;
    }

    public async Task UpdateRecrutamento(Guid recrutamentoId, RecrutamentoUpdateDto recrutamentoNew, bool trackChanges)
    {
        var recrutamento = await _repository.Recrutamento.GetRecrutamentoByIdAsync(recrutamentoId, trackChanges);

        if(recrutamento is null)
        {
            _logger.LogError("Recrutamento com o id '" + recrutamentoId + "' não existe.");
            throw new ArgumentException("Recrutamento com o id '" + recrutamentoId + "' não existe.");
        }

        recrutamento.Nome = recrutamentoNew.Nome ?? recrutamento.Nome;
        recrutamento.Data = recrutamentoNew.Data ?? recrutamento.Data;
        recrutamento.EmpresaID = recrutamentoNew.EmpresaId ?? recrutamento.EmpresaID;

        await _repository.Recrutamento.UpdateRecrutamentoAsync(recrutamento, trackChanges);
    }
}
