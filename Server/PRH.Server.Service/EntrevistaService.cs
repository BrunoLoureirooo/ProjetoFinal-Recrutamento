using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Entrevista;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.RequestFeatures;
using PRH.Server.Entities.Models;

namespace PRH.Server.Service;
public class EntrevistaService : IEntrevistaService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IEmailManager _email;

    public EntrevistaService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IEmailManager email)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
        _email = email;
    }

    public async Task<Guid> GetEmpresaOfEntrevistaById(Guid id, bool trackChanges)
    {
        return await _repository.Entrevista.GetEmpresaOfEntrevistaByIdAsync(id, trackChanges);
    }

    public async Task<EntrevistaGetOneDto> GetEntrevistaById(Guid id, bool trackChanges)
    {
        var entrevista = await _repository.Entrevista.GetEntrevistaByIdAsync(id, trackChanges);

        if (entrevista == null)
        {
            _logger.LogError($"Entrevista com o id '{id}' não existe.");
            throw new ArgumentException($"Entrevista com o id '{id}' não existe.");
        }

        var entrevistaDto = _mapper.Map<EntrevistaGetOneDto>(entrevista);
        return entrevistaDto;
    }

    public async Task<IEnumerable<EntrevistaGetAllDto>> GetEntrevistas(EntrevistaParameters param, bool trackChanges)
    {
        if (param.After is not null && param.Before is not null && !param.ValidDates)
        {
            _logger.LogError("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
            throw new ArgumentException("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
        }

        if (param.ResultadoId is not null && !await _repository.Resultado.CheckIfResultadoExists((Guid)param.ResultadoId, trackChanges))
        {
            _logger.LogError($"Resultado com o id '{param.ResultadoId}' não existe.");
            throw new ArgumentException($"Resultado com o id '{param.ResultadoId}' não existe.");
        }

        var entrevistas = await _repository.Entrevista.GetEntrevistasAsync(param, trackChanges);
        var entrevistasDto = _mapper.Map<IEnumerable<EntrevistaGetAllDto>>(entrevistas);
        return entrevistasDto;
    }

    public async Task<PagedListDto<EntrevistaGetAllDto>> GetEntrevistasPaginated(EntrevistaParameters param, bool trackChanges)
    {
        if (param.After is not null && param.Before is not null && !param.ValidDates)
        {
            _logger.LogError("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
            throw new ArgumentException("Datas não válidas: data 'Before' não pode ser menor ou igual à data 'After'");
        }

        if (param.ResultadoId is not null && !await _repository.Resultado.CheckIfResultadoExists((Guid)param.ResultadoId, trackChanges))
        {
            _logger.LogError($"Resultado com o id '{param.ResultadoId}' não existe.");
            throw new ArgumentException($"Resultado com o id '{param.ResultadoId}' não existe.");
        }

        var entrevistas = await _repository.Entrevista.GetEntrevistasPaginatedAsync(param, trackChanges);
        var entrevistasDto = _mapper.Map<IEnumerable<EntrevistaGetAllDto>>(entrevistas);
        var metaData = _mapper.Map<MetaDataDto>(entrevistas.MetaData);

        return new PagedListDto<EntrevistaGetAllDto>
        {
            Items = [.. entrevistasDto],
            MetaData = metaData
        };
    }

    public async Task<EntrevistaGetOneDto> CreateEntrevista(EntrevistaCreateDto dto, bool trackChanges)
    {
        if (dto == null)
        {
            _logger.LogError("Entrevista vazia não permitida.");
            throw new ArgumentException("Entrevista vazia não permitida.");
        }

        Guid newEntrevistaId = Guid.NewGuid();

        var resultados = await _repository.Resultado.GetResultadosAsync(false);
        var resultado = resultados.Where(r => r.Descricao == "Entrevista Agendada").FirstOrDefault();

        if (resultado == null)
        {
            _logger.LogError("Erro interno ao resgatar resultado 'Entrevista Agendada'.");
            throw new ApplicationException("Erro interno ao resgatar resultado 'Entrevista Agendada'.");
        }

        // Validate candidatura
        var candidatura = await _repository.Candidatura.GetCandidaturaByIdAsync(dto.CandidaturaId, false);

        if(candidatura == null)
        {
            _logger.LogError("Candidatura com o id '" + dto.CandidaturaId + "' não existe.");
            throw new ArgumentException("Candidatura com o id '" + dto.CandidaturaId + "' não existe.");
        }

        if(!candidatura.Estado.Descricao.Equals("A Decorrer"))
        {
            _logger.LogError("Impossivel marcar entrevista para uma candidatura que já não está a decorrer.");
            throw new ArgumentException("Impossivel marcar entrevista para uma candidatura que já não está a decorrer.");
        }

        // Validate entrevistador
        var entrevistador = await _repository.Funcionario.GetFuncionarioByIdAsync(dto.EntrevistadorId, false);
        if (entrevistador == null )
        {
            _logger.LogError("Funcionario com o id '" + dto.EntrevistadorId + "' não existe.");
            throw new ArgumentException("Funcionario com o id '" + dto.EntrevistadorId + "' não existe.");
        }

        if(!entrevistador.EmpresaId.Equals(candidatura.Vaga.Recrutamento.EmpresaID))
        {
            _logger.LogError("Impossivel marcar entrevista para entrevistador de outra empresa.");
            throw new ArgumentException("Impossivel marcar entrevista para entrevistador de outra empresa.");
        }

        Entrevista entrevista = new()
        {
            Id = newEntrevistaId,
            CandidaturaId = dto.CandidaturaId,
            EntrevistadorId = dto.EntrevistadorId,
            DataHora = dto.DataHora,
            Notas = "",
            ResultadoEntrevistaId = resultado.Id
        };

        await _repository.Entrevista.CreateEntrevistaAsync(entrevista, trackChanges);

        _email.SendEntrevistaEmail(candidatura, entrevistador, dto.DataHora);

        EntrevistaGetOneDto entrevistaDto = _mapper.Map<EntrevistaGetOneDto>(entrevista);
        return entrevistaDto;
    }

    public async Task UpdateEntrevistaAsync(Guid entrevistaId, EntrevistaUpdateDto entrevistaNew, bool trackChanges)
    {
        var entrevista = await _repository.Entrevista.GetEntrevistaByIdAsync(entrevistaId, trackChanges);

        if (entrevista is null)
        {
            _logger.LogError("Entrevista com o id '" + entrevistaId + "' não existe.");
            throw new ArgumentException("Entrevista com o id '" + entrevistaId + "' não existe.");
        }
        if(entrevista.Resultado.Descricao != "Entrevista Agendada")
        {
            _logger.LogError("Entrevista com o id '" + entrevistaId + "' já foi terminada.");
            throw new ArgumentException("Entrevista com o id '" + entrevistaId + "' já foi terminada.");
        }
        if (entrevista.Candidatura.Estado.Descricao != "A Decorrer")
        {
            _logger.LogError("Candidatura a entrevista com o id '" + entrevistaId + "' já foi processada.");
            throw new ArgumentException("Candidatura a entrevista com o id '" + entrevistaId + "' já foi processada.");
        }

        entrevista.EntrevistadorId = entrevistaNew.EntrevistadorId ?? entrevista.EntrevistadorId;
        entrevista.Notas = entrevistaNew.Notas ?? entrevista.Notas;
        entrevista.DataHora = entrevistaNew.DataHora ?? entrevista.DataHora;
        if (entrevistaNew.ResultadoId.HasValue)
        {
            var resultado = await _repository.Resultado.GetResultadoByIdAsync(entrevistaNew.ResultadoId.Value, false);
            entrevista.ResultadoEntrevistaId = resultado.Id;
            entrevista.Resultado = resultado;
        }

        await _repository.Entrevista.UpdateEntrevistaAsync(entrevista, trackChanges);

    }


}
