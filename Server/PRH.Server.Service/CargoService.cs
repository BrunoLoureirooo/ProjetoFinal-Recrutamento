using AutoMapper;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Cargo;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.RequestFeatures;
using System.Collections.ObjectModel;

namespace PRH.Server.Service;

public class CargoService : ICargoService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CargoService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CargoGetAllDto>> GetCargos(bool trackChanges)
    {
        var cargos = await _repository.Cargo.GetCargosAsync(trackChanges);

        var cargoDtos = _mapper.Map<IEnumerable<CargoGetAllDto>>(cargos);

        return cargoDtos;
    }
    public async Task<CargoGetOneDto> GetCargoByIdAsync(Guid id, bool trackChanges)
    {
        var cargo = await _repository.Cargo.GetCargoByIdAsync(id, trackChanges);
        if (cargo == null)
        {
            _logger.LogError("Cargo com o id '" + id + "' não existe.");
            throw new ArgumentException("Cargo com o id '" + id + "' não existe.");
        }

        var cargoDto = _mapper.Map<CargoGetOneDto>(cargo);
        return cargoDto;
    }

    public async Task<PagedListDto<CargoGetAllDto>> GetCargosPaginated(CargoParameters param, bool trackChanges)
    {
        var cargos = await _repository.Cargo.GetCargosPaginatedAsync(param, trackChanges);

        var cargosDtos = _mapper.Map<IEnumerable<CargoGetAllDto>>(cargos);
        var metaData = _mapper.Map<MetaDataDto>(cargos.MetaData);
        var response = new PagedListDto<CargoGetAllDto> { Items = [.. cargosDtos], MetaData = metaData };

        return response;
    }

    public async Task<Guid> CreateCargo(CargoCreateDto dto, bool trackChanges)
    {
        if (dto == null)
        {
            _logger.LogError("Recrutamento vazio não permitido.");
            throw new ArgumentException("Recrutamento vazio não permitido.");
        }

        Guid newCargoId = Guid.NewGuid();
        Collection<DocumentosNecessarios> documentosNecessarios = [];

        foreach(Guid tipoId in dto.DocumentosNecessarios)
        {
            TipoDocumento t = await _repository.TipoDocumento.GetTipoDocumentoByIdAsync(tipoId, trackChanges);
            if (t is null)
            {
                _logger.LogError("Tipo de documento com o id '" + tipoId + "' não existe.");
                throw new ArgumentException("Tipo de documento com o id '" + tipoId + "' não existe.");
            }
            documentosNecessarios.Add(new DocumentosNecessarios { Id = Guid.NewGuid(), CargoId = newCargoId, TipoDocumentoId = t.Id});
        }

        Cargo cargo = new()
        {
            Id = newCargoId,
            Nome = dto.Nome,
            DocumentosNecessarios = documentosNecessarios
        };

        await _repository.Cargo.CreateCargoAsync(cargo, trackChanges);

        return newCargoId;
    }
}
