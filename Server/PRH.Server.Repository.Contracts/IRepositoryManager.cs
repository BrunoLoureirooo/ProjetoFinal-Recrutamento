using PRH.Server.Repository.Contracts.Lookups;
using System;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Repository.Contracts;

public interface IRepositoryManager
{
    IEmpresaRepository Empresa { get; }
    ITemplateRepository Template { get; }
    ITipoDocumentoRepository TipoDocumento { get; }
    IDocumentoRepository Documento { get; }
    IRecrutamentoRepository Recrutamento { get; }
	IFormularioRepository Formulario { get; }
    IFormularioRespondidoRepository FormularioRespondido { get; }
    ICargoRepository Cargo { get; }
    ITipoVagaRepository TipoVaga { get; }
    ITipoCampoRepository TipoCampo { get; }
    ITipoFormularioRepository TipoFormulario { get; }
    IModalidadeRepository Modalidade { get; }
    IEstadoRepository Estado { get; }
    IResultadoRepository Resultado { get; }
	IVagaRepository Vaga { get; }
    IVagaFormularioRepository VagaFormulario { get; }
    ICampoRepository Campo { get; }
    IFormularioCampoRepository FormularioCampo { get; }
    ICandidaturaRepository Candidatura { get; }
    IEntrevistaRepository Entrevista { get; }
    IFuncionarioRepository Funcionario { get; }
    IGestorClienteRepository GestorCliente { get; }
    Task SaveAsync();
}

