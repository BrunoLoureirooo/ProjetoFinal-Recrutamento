using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.TipoDocumento;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.RequestFeatures;
using System.Collections.ObjectModel;

namespace PRH.Server.Service;
public class VagaService : IVagaService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public VagaService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<VagaGetOneDto> GetVagaById(Guid id, bool trackChanges)
    {
        var vaga = await _repository.Vaga.GetVagaByIdAsync(id, trackChanges);

        if (vaga == null)
        {
            _logger.LogError("Vaga com o id '" + id + "' não existe.");
            throw new ArgumentException("Vaga com o id '" + id + "' não existe.");
        }

        var vagaDto = _mapper.Map<VagaGetOneDto>(vaga);

        return vagaDto;
    }

    public async Task<IEnumerable<VagaGetAllDto>> GetVagasByEmpresa(Guid empresaId, bool trackChanges)
    {
        if (!await _repository.Empresa.CheckIfEmpresaExists(empresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + empresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + empresaId + "' não existe.");
        }

        var vagas = await _repository.Vaga.GetVagasByEmpresaAsync(empresaId, trackChanges);

        var vagasDto = _mapper.Map<IEnumerable<VagaGetAllDto>>(vagas);

        return vagasDto;
    }

    public async Task<PagedListDto<VagaGetAllDto>> GetVagasByEmpresaPaginated(Guid empresaId, VagaParameters param, bool trackChanges)
    {
        if (!await _repository.Empresa.CheckIfEmpresaExists(empresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + empresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + empresaId + "' não existe.");
        }

        var vagas = await _repository.Vaga.GetVagasByEmpresaPaginatedAsync(empresaId, param, trackChanges);

        var vagasDto = _mapper.Map<IEnumerable<VagaGetAllDto>>(vagas);
        var metaData = _mapper.Map<MetaDataDto>(vagas.MetaData);
        var response = new PagedListDto<VagaGetAllDto> { Items = [.. vagasDto], MetaData = metaData };

        return response;
    }

    public async Task<IEnumerable<TipoDocumentoGetVagaRequirementsDto>> GetRequiredFilesByVaga(Guid id, bool trackChanges)
    {
        var vaga = await _repository.Vaga.GetVagaByIdAsync(id, trackChanges);

        if (vaga == null)
        {
            _logger.LogError("Vaga com o id '" + id + "' não existe.");
            throw new ArgumentException("Vaga com o id '" + id + "' não existe.");
        }

        var requirementsDto = _mapper.Map<IEnumerable<TipoDocumentoGetVagaRequirementsDto>>(vaga.Cargo.DocumentosNecessarios.Select(dn => dn.TipoDocumento));

        return requirementsDto;
    }

    public async Task<IEnumerable<FormularioGetVagaFormsDto>> GetFormsByVaga(Guid id, bool trackChanges)
    {
        var vaga = await _repository.Vaga.GetVagaByIdAsync(id, trackChanges);

        if (vaga == null)
        {
            _logger.LogError("Vaga com o id '" + id + "' não existe.");
            throw new ArgumentException("Vaga com o id '" + id + "' não existe.");
        }

        var formsDto = _mapper.Map<IEnumerable<FormularioGetVagaFormsDto>>(vaga.VagasFormularios.Select(vf => vf.Formulario));

        return formsDto;
    }

    public async Task<VagaGetOneDto> CreateVaga(VagaCreateDto dto, bool trackChanges)
    {
        if (dto == null)
        {
            _logger.LogError("Vaga vazia não permitido.");
            throw new ArgumentException("Vaga vazia não permitido.");
        }

        if (!await _repository.Recrutamento.CheckIfRecrutamentoExists((Guid)dto.RecrutamentoId, trackChanges))
        {
            _logger.LogError("Recrutamento com o id '" + dto.RecrutamentoId + "' não existe.");
            throw new ArgumentException("Recrutamento com o id '" + dto.RecrutamentoId + "' não existe.");
        }

        Guid newVagaId = Guid.NewGuid();

        Vaga vaga = new()
        {
            Id = newVagaId,
            RecrutamentoId = (Guid)dto.RecrutamentoId,
            CandidaturaDisponivel = dto.CandidaturaDisponivel,
            TipoId = (Guid)dto.TipoVagaId,
            ModalidadeId = (Guid)dto.ModalidadeId,
            CargoId = (Guid)dto.CargoId,
            NumVagas = dto.NumVagas,
            Descricao = dto.Descricao,
            Candidaturas = null,
            VagasFormularios = new Collection<VagaFormulario>([.. dto.FormularioIds.Select(f => new VagaFormulario
            {
                Id = Guid.NewGuid(),
                VagaId = newVagaId,
                FormularioId = f
            })])
        };

        await _repository.Vaga.CreateVagaAsync(vaga, trackChanges);

        VagaGetOneDto vagaDto = _mapper.Map<VagaGetOneDto>(vaga);
        return vagaDto;
    }

    public async Task<bool> VagaBelongsToEmpresa(Guid vagaId, Guid empresaId, bool trackChanges)
    {
        var vaga = await _repository.Vaga.GetVagaByIdAsync(vagaId, trackChanges);

        if (vaga.Recrutamento.Empresa.Id.Equals(empresaId)) return true;

        return false;
    }

    public async Task UpdateVagaAsync(Guid vagaId, VagaUpdateDto vagaNew, bool trackChanges)
    {
        var vaga = await _repository.Vaga.GetVagaByIdAsync(vagaId, trackChanges);

        if (vaga is null)
        {
            _logger.LogError("Vaga com o id '" + vagaId + "' não existe.");
            throw new ArgumentException("Vaga com o id '" + vagaId + "' não existe.");
        }

        vaga.CandidaturaDisponivel = vagaNew.CandidaturaDisponivel ?? vaga.CandidaturaDisponivel;
        vaga.NumVagas = vagaNew.NumVagas ?? vaga.NumVagas;
        vaga.Descricao = vagaNew.Descricao ?? vaga.Descricao;
        vaga.ModalidadeId = vagaNew.ModalidadeId ?? vaga.ModalidadeId;
        vaga.TipoId = vagaNew.TipoVagaId ?? vaga.TipoId;

        await _repository.Vaga.UpdateVagaAsync(vaga, trackChanges);

        foreach(var f in vagaNew.FormularioIds)
        {
            VagaFormulario newVagaFormulario = new VagaFormulario
            {
                FormularioId = f,
                VagaId = vagaId
            };
            await _repository.VagaFormulario.CreateVagaFormularioAsync(newVagaFormulario, trackChanges);
        }
    }
    
}
