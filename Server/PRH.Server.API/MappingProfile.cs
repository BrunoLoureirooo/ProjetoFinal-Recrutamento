 using AutoMapper;
using PRH.Server.Entities.Models;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.CampoRespondido;
using PRH.Shared.DataTransferObjets.Entrevista;
using PRH.Shared.DataTransferObjets.Funcionario;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.Campo;
using PRH.Shared.DataTransferObjets.Admissao;
using PRH.Shared.RequestFeatures;
using PRH.Shared.DataTransferObjets.TipoDocumento;
using PRH.Shared.DataTransferObjets.Campos;
using PRH.Shared.DataTransferObjets.Cargo;

namespace PRH.Server.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de Usuário
            CreateMap<UserForRegistrationDto, ApplicationUser>();

            // Mapeamentos Básicos
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<Template, TemplateDto>()
                .ForMember(dest => dest.TipoDocumentoNome, opt => opt.MapFrom(src => src.TipoDocumento.Nome))
                .ForMember(dest => dest.EmpresaNome, opt => opt.MapFrom(src => src.Empresa.Nome));
            CreateMap<TipoDocumento, TipoDocumentoDto>();
            CreateMap<Documento, DocumentoDto>();
            CreateMap<EmpresaTipoDocumento, EmpresaTipoDocumentoDto>();
            CreateMap<MetaData, MetaDataDto>();

            /* Mapping de Cargos */

            CreateMap<Cargo, CargoGetAllDto>()
                .ForMember(dest => dest.NumDocumentosNecessarios, opt => opt.MapFrom(src => src.DocumentosNecessarios.Count()));
            CreateMap<Cargo, CargoGetOneDto>()
                .ForMember(dest => dest.TiposDocumento, opt => opt.MapFrom(src => src.DocumentosNecessarios.Select(x => x.TipoDocumento)));

            /* Mapping de Campos */

            CreateMap<Campo, CampoGetDto>()
                .ForMember(dest => dest.TipoCampo, opt => opt.MapFrom(src => src.TipoCampo));
            CreateMap<Campo, CampoCreateDto>()
                .ForMember(dest => dest.TipoCampo, opt => opt.MapFrom(src => src.TipoCampo));
            CreateMap<Campo, CampoGetDto>()
                .ForMember(dest => dest.TipoCampo, opt => opt.MapFrom(src => src.TipoCampo));
            CreateMap<CampoRespondido, CampoRespondidoDto>();
            CreateMap<CampoRespondido, CampoRespondidoGetOneCandidaturaDto>();
            CreateMap<Campo, CampoGetVagaFormsDto>()
                .ForMember(dest => dest.OpcoesCampoLookup, opt => opt.MapFrom(src => src.OpcoesCampoLookups.Select(ocl => ocl.Descricao)))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoCampo.Descricao));

            /* Mapping de Candidaturas */

            CreateMap<Candidatura, CandidaturaGetAllDto>()
                .ForMember(dest => dest.EmpresaNome, opt => opt.MapFrom(src => src.Vaga.Recrutamento.Empresa.Nome))
                .ForMember(dest => dest.RecrutamentoNome, opt => opt.MapFrom(src => src.Vaga.Recrutamento.Nome))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado));

            CreateMap<Candidatura, CandidaturaGetOneDto>()
                .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Vaga.Recrutamento.Empresa))
                .ForMember(dest => dest.RecrutamentoNome, opt => opt.MapFrom(src => src.Vaga.Recrutamento.Nome))
                .ForMember(dest => dest.Vaga, opt => opt.MapFrom(src => src.Vaga))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.CamposRespondidos, opt => opt.MapFrom(src => src.FormularioRespondidos.SelectMany(fr => fr.CampoRespondidos)))
                .ForMember(dest => dest.Entrevistas, opt => opt.MapFrom(src => src.Entrevistas));

            CreateMap<Candidatura, CandidaturaGetOneVagaDto>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado));
                
            CreateMap<Candidatura, CandidaturaGetOneEntrevistaDto>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Descricao));

            CreateMap<CandidaturaUpdateDto, Candidatura>();

            /* Mapping de Lookups */

            CreateMap<TipoVaga, LookupDto>();
            CreateMap<Modalidade, LookupDto>();
            CreateMap<TipoFormulario, LookupDto>();
            CreateMap<Resultado, LookupDto>();

            CreateMap<LookupDto, TipoFormulario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<LookupDto, TipoCampo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Lookup, LookupDto>();

            /* Mapping Vagas */

            CreateMap<Vaga, VagaGetAllDto>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.TipoVaga, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Modalidade, opt => opt.MapFrom(src => src.Modalidade));

            CreateMap<Vaga, VagaGetOneDto>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.TipoVaga, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Modalidade, opt => opt.MapFrom(src => src.Modalidade))
                .ForMember(dest => dest.Formularios, opt => opt.MapFrom(src => src.VagasFormularios.Select(vf => vf.Formulario)))
                .ForMember(dest => dest.Candidaturas, opt => opt.MapFrom(src => src.Candidaturas));

            CreateMap<Vaga, VagaGetOneRecrutamentoDto>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.TipoVaga, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Modalidade, opt => opt.MapFrom(src => src.Modalidade));

            CreateMap<VagaUpdateDto, Vaga>();

            CreateMap<VagaCreateRecrutamentoDto, VagaCreateDto>()
                .ForMember(dest => dest.FormularioIds, opt => opt.MapFrom(src => src.FormularioIds));

            /* Mapping de Recrutamentos */

            CreateMap<Recrutamento, RecrutamentoGetOneDto>()
                .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
                .ForMember(dest => dest.Vagas, opt => opt.MapFrom(src => src.Vagas))
                .ForMember(dest => dest.TotalVagas, opt => opt.MapFrom(src => src.Vagas.Sum(v => v.NumVagas)))
                .ForMember(dest => dest.Cargos, opt => opt.MapFrom(src => src.Vagas.Select(v => v.Cargo.Nome).Distinct()));

            CreateMap<Recrutamento, RecrutamentoAfterCreateDto>()
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.Empresa.Id));

            CreateMap<Recrutamento, RecrutamentoGetAllDto>()
                .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
                .ForMember(dest => dest.TotalVagas, opt => opt.MapFrom(src => src.Vagas.Sum(v => v.NumVagas)))
                .ForMember(dest => dest.Cargos, opt => opt.MapFrom(src => src.Vagas.Select(v => v.Cargo.Nome).Distinct()));

            /* Mapping de Formularios */

            CreateMap<Formulario, FormularioGetAllDto>();

            CreateMap<Formulario, FormularioGetOneDto>()
                .ForMember(dest => dest.TipoFormulario, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.FormularioCampos, opt => opt.MapFrom(src => src.FormularioCampos));
            CreateMap<Formulario, FormularioGetAllDto>()
                .ForMember(dest => dest.TipoFormulario, opt => opt.MapFrom(src => src.Tipo));
            CreateMap<Formulario, FormularioUpdateDto>()
                .ForMember(dest => dest.TipoFormulario, opt => opt.MapFrom(src => src.Tipo));
            CreateMap<FormularioUpdateDto, Formulario>()
                .ForMember(dest => dest.Tipo, opt => opt.Ignore())
                .ForMember(dest => dest.FormularioCampos, opt => opt.Ignore());

            CreateMap<FormularioCampo, FormularioCampoDto>()
                .ForMember(dest => dest.Campo, opt => opt.MapFrom(src => src.Campo != null
                    ? new CampoGetDto
                    {
                        Id = src.Campo.Id,
                        Nome = src.Campo.Nome,
                        NomeUnico = src.Campo.NomeUnico,
                        IsOpcional = src.Campo.IsOpcional,
                        TipoCampo = src.Campo.TipoCampo != null
                            ? new LookupDto
                            {
                                Id = src.Campo.TipoCampo.Id,
                                Descricao = src.Campo.TipoCampo.Descricao
                            }
                            : null
                    }
                    : null));

            CreateMap<Formulario, FormularioGetVagaFormsDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.Descricao))
                .ForMember(dest => dest.Campos, opt => opt.MapFrom(src => src.FormularioCampos.Where(x => x.Ativo).Select(fc => fc.Campo)));

            /* Mapping de Entrevistas */

            CreateMap<Entrevista, EntrevistaGetAllDto>()
                .ForMember(dest => dest.NomeCandidato, opt => opt.MapFrom(src => src.Candidatura.NomeCandidato))
                .ForMember(dest => dest.Resultado, opt => opt.MapFrom(src => src.Resultado));

            CreateMap<Candidatura, AdmissaoGetAllDto>()
                .ForMember(dest => dest.NomeCandidato, opt => opt.MapFrom(src => src.NomeCandidato))
                .ForMember(dest => dest.NIFcandidato, opt => opt.MapFrom(src => src.NIFcandidato))
                .ForMember(dest => dest.ContactoCandidato, opt => opt.MapFrom(src => src.ContactoCandidato))
                .ForMember(dest => dest.EmailCandidato, opt => opt.MapFrom(src => src.EmailCandidato))
                .ForMember(dest => dest.RecrutamentoNome, opt => opt.MapFrom(src => src.Vaga.Recrutamento.Nome));

            CreateMap<Entrevista, EntrevistaGetOneDto>()
                .ForMember(dest => dest.Candidatura, opt => opt.MapFrom(src => src.Candidatura))
                .ForMember(dest => dest.Entrevistador, opt => opt.MapFrom(src => src.Entrevistador))
                .ForMember(dest => dest.Resultado, opt => opt.MapFrom(src => src.Resultado));

            CreateMap<EntrevistaCreateDto, Entrevista>();

            CreateMap<EntrevistaUpdateDto, Entrevista>();

            /* Mapping de Funcionarios */

            CreateMap<Funcionario, FuncionarioGetOneEntrevistaDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ApplicationUser.Nome));
            CreateMap<Funcionario, FuncionarioGetAllDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ApplicationUser.Nome))
                .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa.Nome));
            CreateMap<Funcionario, FuncionarioGetOneDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApplicationUser.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ApplicationUser.Nome));

            /* Mapping de Tipos de Documentos */

            CreateMap<TipoDocumento, TipoDocumentoCreateCandidaturaDto>();
            CreateMap<TipoDocumento, TipoDocumentoGetOneCargoDto>();
            CreateMap<TipoDocumento, TipoDocumentoGetVagaRequirementsDto>();
        }
    }
}