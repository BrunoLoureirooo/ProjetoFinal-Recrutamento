using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.TipoDocumento;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Service;

public class CandidaturaService : ICandidaturaService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentoService _documentoService;
    public CandidaturaService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IDocumentoService documentoService)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
        _documentoService = documentoService;
    }

    public async Task<CandidaturaGetOneDto> CreateCandidatura(CandidaturaCreateDto dto, bool trackChanges)
    {
        if (dto == null)
        {
            _logger.LogError("Candidatura vazia não permitido.");
            throw new ArgumentException("Candidatura vazia não permitido.");
        }

        Vaga vaga = await _repository.Vaga.GetVagaByIdAsync((Guid)dto.VagaId, trackChanges);

        if (vaga is null)
        {
            _logger.LogError("Vaga com o id '" + dto.VagaId + "' não existe.");
            throw new ArgumentException("Vaga com o id '" + dto.VagaId + "' não existe.");
        }

        if(!vaga.CandidaturaDisponivel || vaga.NumVagas <= 0)
        {
            _logger.LogError("Vaga com o id '" + dto.VagaId + "' não está disponível para candidaturas.");
            throw new ArgumentException("Vaga com o id '" + dto.VagaId + "' não está disponível para candidaturas.");
        }

        if(!ValidateNIF(dto.NIFcandidato))
        {
            _logger.LogError("NIF do candidato não válido");
            throw new ArgumentException("NIF do candidato não válido");
        }

        Guid newCandidaturaId = Guid.NewGuid();
        Collection<FormularioRespondido> formsRespondidos = [];
        Collection<FicheiroComTipo> ficheiros = [];

        var tiposDocumento = await _repository.TipoDocumento.GetTipoDocumentosAsync(trackChanges);
        var tipoDocumentoAnexo = tiposDocumento.Where(x => x.Nome.Equals("Anexo")).FirstOrDefault();

        if (tipoDocumentoAnexo is null)
        {
            _logger.LogError("Erro interno ao associar anexos da nova candidatura a um tipo de documento");
            throw new ApplicationException("Erro interno ao associar anexos da nova candidatura a um tipo de documento");
        }

        // Verifica que existe uma resposta na candidatatura para todos os campos obrigatorios da vaga
        foreach (Formulario formNeeded in vaga.VagasFormularios.Select(vf => vf.Formulario))
        {
            FormularioRespondido formRespondido = new()
            {
                Id = Guid.NewGuid(),
                Nome = formNeeded.Nome,
                CandidaturaId = newCandidaturaId,
                Tipo = formNeeded.Tipo.Descricao,
                CampoRespondidos = []
            };

            foreach(Campo campoNeeded in formNeeded.FormularioCampos.Where(x => x.Ativo).Select(fc => fc.Campo))
            {
                CampoRespondidoCreateCandidaturaDto campoRespondidoFound = null;
                foreach(CampoRespondidoCreateCandidaturaDto campoRespondido in dto.CamposRespondidos)
                {
                    if(campoRespondido.CampoId.Equals(campoNeeded.Id))
                    {
                        campoRespondidoFound = campoRespondido;
                        break;
                    }
                }
                if(campoRespondidoFound is null)
                {
                    if (!campoNeeded.IsOpcional)
                    {
                        _logger.LogError("Nem todos os campos obrigatórios foram respondidos");
                        throw new ArgumentException("Nem todos os campos obrigatórios foram respondidos");
                    }

                    campoRespondidoFound = new CampoRespondidoCreateCandidaturaDto
                    {
                        CampoId = campoNeeded.Id,
                        Resposta = "N/A"
                    };
                }

                // adicionar documentos e uploads caso o campo seja um anexo e tenha resposta
                else if (campoNeeded.TipoCampo.Descricao.Equals("anexo"))
                {
                    var ficheiro = Base64ToFormFile(campoRespondidoFound.Resposta, DateTime.Now + ".pdf");
                    ficheiros.Add(new FicheiroComTipo(ficheiro, tipoDocumentoAnexo.Id));
                    campoRespondidoFound.Resposta = "Anexo enviado!";
                }

                // verificar campos do tipo lookup
                else if (campoNeeded.TipoCampo.Descricao.Equals("lookup"))
                {
                    bool lookupFound = false;
                    foreach(var option in campoNeeded.OpcoesCampoLookups)
                    {
                        if (option.Descricao.ToLower().Equals(campoRespondidoFound.Resposta.ToLower()))
                        {
                            lookupFound = true;
                            break;
                        }
                    }

                    if(!lookupFound)
                    {
                        string message = "Campo '" + campoNeeded.Id + "' do tipo 'lookup' com resposta fora das opções possiveis:\n";
                        foreach (var option in campoNeeded.OpcoesCampoLookups)
                            message += '\t' + option.Descricao + ";\n";

                        _logger.LogError(message);
                        throw new ArgumentException(message);
                    }
                }

                formRespondido.CampoRespondidos.Add(new CampoRespondido
                {
                    Id = Guid.NewGuid(),
                    Nome = campoNeeded.Nome,
                    Tipo = campoNeeded.TipoCampo.Descricao,
                    Resposta = campoRespondidoFound.Resposta,
                    FormularioRespondidoId = formRespondido.Id
                });
            }

            formsRespondidos.Add(formRespondido);
        }

        // Verifica que existe um documento na candidatatura para todos os documentos obrigatorios do cargo da vaga
        foreach (TipoDocumento documentoNeeded in vaga.Cargo.DocumentosNecessarios.Select(dn => dn.TipoDocumento))
        {
            TipoDocumentoCreateCandidaturaDto documentoRespondidoFound = null;
            foreach (TipoDocumentoCreateCandidaturaDto documentoRespondido in dto.Documentos)
            {
                if (documentoRespondido.TipoDocumentoId.Equals(documentoNeeded.Id))
                {
                    documentoRespondidoFound = documentoRespondido;
                    break;
                }
            }

            if (documentoRespondidoFound is null)
            {
                _logger.LogError("Nem todos os documentos necessários foram enviados");
                throw new ArgumentException("Nem todos os documentos necessários foram enviados");
            }

            var ficheiro = Base64ToFormFile(documentoRespondidoFound.Ficheiro, DateTime.Now + ".pdf");
            ficheiros.Add(new FicheiroComTipo(ficheiro, documentoRespondidoFound.TipoDocumentoId));
        }

        var estados = await _repository.Estado.GetEstadosAsync(trackChanges);
        var estadoCandidatura = estados.Where(e => e.Descricao.Equals("A Decorrer")).FirstOrDefault();

        if(estadoCandidatura is null)
        {
            _logger.LogError("Erro interno ao associar nova candidatura ao estado 'Candidatura'");
            throw new ApplicationException("Erro interno ao associar nova candidatura ao estado 'Candidatura'");
        }

        Candidatura candidatura = new()
        {
            Id = newCandidaturaId,
            VagaId = (Guid)dto.VagaId,
            EstadoId = estadoCandidatura.Id,
            NIFcandidato = dto.NIFcandidato,
            NomeCandidato = dto.NomeCandidato,
            EmailCandidato = dto.EmailCandidato,
            ContactoCandidato = dto.ContactoCandidato,
            FormularioRespondidos = formsRespondidos
        };

        await _repository.Candidatura.CreateCandidaturaAsync(candidatura, trackChanges);

        foreach(var f in ficheiros)
            await _documentoService.UploadDocumentoAsync(candidatura.Id, f.Ficheiro, f.TipoId);

        var candidaturaDto = _mapper.Map<CandidaturaGetOneDto>(candidatura);

        return candidaturaDto;
    }

    public async Task<CandidaturaGetOneDto> GetCandidaturaById(Guid id, bool trackChanges)
    {
        var candidatura = await _repository.Candidatura.GetCandidaturaByIdAsync(id, trackChanges);

        if (candidatura == null)
        {
            _logger.LogError("Candidatura com o id '" + id + "' não existe.");
            throw new ArgumentException("Candidatura com o id '" + id + "' não existe.");
        }

        var candidaturaDto = _mapper.Map<CandidaturaGetOneDto>(candidatura);

        return candidaturaDto;
    }

    public async Task<IEnumerable<CandidaturaGetAllDto>> GetCandidaturas(CandidaturaParameters param, bool trackChanges)
    {
        await CheckEverything(param.EmpresaId, param.EstadoId, trackChanges);

        var candidaturas = await _repository.Candidatura.GetCandidaturasAsync(param, trackChanges);

        var candidaturasDto = _mapper.Map<IEnumerable<CandidaturaGetAllDto>>(candidaturas);

        return candidaturasDto;
    }

    public async Task<IEnumerable<CandidaturaGetAllDto>> GetCandidaturasByEmpresa(Guid empresaId, CandidaturaParameters param, bool trackChanges)
    {
        if (!await _repository.Empresa.CheckIfEmpresaExists(empresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + empresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + empresaId + "' não existe.");
        }

        var candidaturas = await _repository.Candidatura.GetCandidaturasByEmpresaAsync(empresaId, param, trackChanges);

        var candidaturasDto = _mapper.Map<IEnumerable<CandidaturaGetAllDto>>(candidaturas);

        return candidaturasDto;
    }

    public async Task<PagedListDto<CandidaturaGetAllDto>> GetCandidaturasByEmpresaPaginated(Guid empresaId, CandidaturaParameters param, bool trackChanges)
    {
        await CheckEverything(empresaId, param.EstadoId, trackChanges);

        var candidaturas = await _repository.Candidatura.GetCandidaturasByEmpresaPaginatedAsync(empresaId, param, trackChanges);

        var candidaturasDto = _mapper.Map<IEnumerable<CandidaturaGetAllDto>>(candidaturas);
        var metaData = _mapper.Map<MetaDataDto>(candidaturas.MetaData);
        var response = new PagedListDto<CandidaturaGetAllDto> { Items = candidaturasDto.ToList(), MetaData = metaData };

        return response;
    }

    public async Task<PagedListDto<CandidaturaGetAllDto>> GetCandidaturasPaginated(CandidaturaParameters param, bool trackChanges)
    {
        await CheckEverything(param.EmpresaId, param.EstadoId, trackChanges);

        var candidaturas = await _repository.Candidatura.GetCandidaturasPaginatedAsync(param, trackChanges);

        var candidaturaDtos = _mapper.Map<IEnumerable<CandidaturaGetAllDto>>(candidaturas);
        var metaData = _mapper.Map<MetaDataDto>(candidaturas.MetaData);
        var response = new PagedListDto<CandidaturaGetAllDto> { Items = [.. candidaturaDtos], MetaData = metaData };

        return response;
    }

    public async Task<PagedListDto<AdmissaoGetAllDto>> GetAdmissaoPaginated(AdmissaoParameters param, bool trackChanges)
    {
        var candidaturas = await _repository.Candidatura.GetAdmissaoPaginatedAsync(param, trackChanges);
        var candidaturasDto = _mapper.Map<IEnumerable<AdmissaoGetAllDto>>(candidaturas);
        var metaDataDto = _mapper.Map<MetaDataDto>(candidaturas.MetaData);

        return new PagedListDto<AdmissaoGetAllDto>
        {
            Items = [.. candidaturasDto],
            MetaData = metaDataDto
        };
    }

    public async Task<IEnumerable<AdmissaoGetAllDto>> GetAdmissao(AdmissaoParameters param, bool trackChanges)
    {
        var candidaturas = await _repository.Candidatura.GetAdmissaoAsync(param, trackChanges);
        var candidaturasDto = _mapper.Map<IEnumerable<AdmissaoGetAllDto>>(candidaturas);
        return candidaturasDto;
    }


    public async Task UpdateCandidaturaAsync(Guid candidaturaId, CandidaturaUpdateDto candidaturaNew, bool trackChanges)
    {
        var candidatura = await _repository.Candidatura.GetCandidaturaByIdAsync(candidaturaId, trackChanges);

        if (candidatura is null)
        {
            _logger.LogError("Candidatura com o id '" + candidaturaId + "' não existe.");
            throw new ArgumentException("Candidatura com o id '" + candidaturaId + "' não existe.");
        }

        candidatura.EstadoId = candidaturaNew.EstadoId ?? candidatura.EstadoId;

        await _repository.Candidatura.UpdateCandidaturaAsync(candidatura, trackChanges);
	}

    private async Task CheckEverything(Guid? empresaId, Guid? estadoId, bool trackChanges)
    {
        if (estadoId is not null && !await _repository.Estado.CheckIfEstadoExists((Guid)estadoId, trackChanges))
        {
            _logger.LogError("Estado com o id '" + estadoId + "' não existe.");
            throw new ArgumentException("Estado com o id '" + estadoId + "' não existe.");
        }

        if (empresaId is not null && !await _repository.Empresa.CheckIfEmpresaExists((Guid)empresaId, trackChanges))
        {
            _logger.LogError("Empresa com o id '" + empresaId + "' não existe.");
            throw new ArgumentException("Empresa com o id '" + empresaId + "' não existe.");
        }
    }

    private bool ValidateNIF(int nif)
    {
        string nifStr = nif.ToString();
        double control = 
            Char.GetNumericValue(nifStr.ElementAt(7)) * 2 +
            Char.GetNumericValue(nifStr.ElementAt(6)) * 3 +
            Char.GetNumericValue(nifStr.ElementAt(5)) * 4 +
            Char.GetNumericValue(nifStr.ElementAt(4)) * 5 +
            Char.GetNumericValue(nifStr.ElementAt(3)) * 6 +
            Char.GetNumericValue(nifStr.ElementAt(2)) * 7 +
            Char.GetNumericValue(nifStr.ElementAt(1)) * 8 +
            Char.GetNumericValue(nifStr.ElementAt(0)) * 9;

        if (control % 11 <= 1) control = 0;
        else control = 11 - control % 11;

        if (control != Char.GetNumericValue(nifStr.ElementAt(8))) return false;

        return true;
    }

    public IFormFile Base64ToFormFile(string base64String, string fileName)
    {
        var base64Data = base64String.Contains(',') ? base64String.Split(',')[1] : base64String;
        var fileBytes = Convert.FromBase64String(base64Data);

        // verifica que o ficheiro enviado é um pdf
        if(!(fileBytes.Length >= 4 &&
               fileBytes[0] == 0x25 && // %
               fileBytes[1] == 0x50 && // P
               fileBytes[2] == 0x44 && // D
               fileBytes[3] == 0x46    // F
           ))
        {
            _logger.LogError("Ficheiro enviado não é um .pdf");
            throw new ArgumentException("Ficheiro enviado não é um .pdf");
        }

        var stream = new MemoryStream(fileBytes);
        return new FormFile(stream, 0, fileBytes.Length, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };
    }

    private class FicheiroComTipo
    {
        public IFormFile Ficheiro { get; set; }
        public Guid TipoId { get; set; }

        public FicheiroComTipo(IFormFile ficheiro, Guid tipoId)
        {
            Ficheiro = ficheiro;
            TipoId = tipoId;
        }
    }
}
