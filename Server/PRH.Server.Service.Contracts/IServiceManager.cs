using PRH.Server.Service.Contracts.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Service.Contracts
{
    public interface IServiceManager
    {
        IEmpresaService EmpresaService { get; }
        ITemplateService TemplateService { get; }
        ITipoDocumentoService TipoDocumentoService { get; }
        IAuthenticationService AuthenticationService { get; }
        IDocumentoService DocumentoService { get; }
        IRecrutamentoService RecrutamentoService { get; }
        IVagaService VagaService { get; }
        IFormularioService FormularioService { get; }
        IFormularioRespondidoService FormularioRespondidoService { get; }
        ICargoService CargoService { get; }
        ITipoVagaService TipoVagaService { get; }
        ITipoCampoService TipoCampoService { get; }
        ITipoFormularioService TipoFormularioService { get; }
        IModalidadeService ModalidadeService { get; }
        IEstadoService EstadoService { get; }
        IResultadoService ResultadoService { get; }
        ICampoService CampoService { get; }
        IFormularioCampoService FormularioCampoService { get; }
        ICandidaturaService CandidaturaService { get; }
        IEntrevistaService EntrevistaService { get; }
        IFuncionarioService FuncionarioService { get; }
    }
}
