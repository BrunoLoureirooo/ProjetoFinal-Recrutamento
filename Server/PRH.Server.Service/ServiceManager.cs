using AutoMapper;
using PRH.Server.Entities.ConfigurationModels;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service.Contracts.Lookups;
using PRH.Server.Service.Lookups;
using PRH.Server.EmailService;

namespace PRH.Server.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITemplateService> _TemplateService;
        private readonly Lazy<ITipoDocumentoService> _TipoDocumentoService;
        private readonly Lazy<IDocumentoService> _DocumentoService;
        private readonly Lazy<IEmpresaService> _empresaService;
        private readonly Lazy<IRecrutamentoService> _recrutamentoService;
        private readonly Lazy<IFormularioService> _formularioService;
        private readonly Lazy<IFormularioRespondidoService> _formularioRespondidoService;
        private readonly Lazy<IVagaService> _vagaService;
        private readonly Lazy<ICargoService> _cargoService;
        private readonly Lazy<IModalidadeService> _modalidadeService;
        private readonly Lazy<ITipoVagaService> _tipoVagaService;
        private readonly Lazy<ITipoCampoService> _tipoCampoService;
        private readonly Lazy<ITipoFormularioService> _tipoFormularioService;
        private readonly Lazy<IEstadoService> _estadoService;
        private readonly Lazy<IResultadoService> _resultadoService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ICampoService> _campoService;
        private readonly Lazy<IFormularioCampoService> _formularioCampoService;
        private readonly Lazy<ICandidaturaService> _candidaturaService;
        private readonly Lazy<IEntrevistaService> _entrevistaService;
        private readonly Lazy<IFuncionarioService> _funcionarioService;
        private readonly Lazy<IEmailManager> _emailManagerService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, 
            //IDataShaper<EmployeeDto> dataShaper,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env,
            IOptions<JwtConfiguration> configuration)
        {
            _TemplateService = new Lazy<ITemplateService>(() => new TemplateService(repositoryManager, logger, mapper, env));
            _TipoDocumentoService = new Lazy<ITipoDocumentoService>(() => new TipoDocumentoService(repositoryManager, logger, mapper));
            _DocumentoService = new Lazy<IDocumentoService>(() => new DocumentoService(repositoryManager, logger, mapper, env));

            _emailManagerService = new Lazy<IEmailManager>(() => new EmailManager(logger));
            _empresaService = new Lazy<IEmpresaService>(() => new EmpresaService(repositoryManager, logger, mapper));
            _vagaService = new Lazy<IVagaService>(() => new VagaService(repositoryManager, logger, mapper));
            _recrutamentoService = new Lazy<IRecrutamentoService>(() => new RecrutamentoService(repositoryManager, logger, mapper, _vagaService.Value));
            _authenticationService = new Lazy<IAuthenticationService>(() =>new AuthenticationService(logger, mapper, userManager, configuration));
            _formularioService = new Lazy<IFormularioService>(() => new FormularioService(repositoryManager, logger, mapper));
            _formularioRespondidoService = new Lazy<IFormularioRespondidoService>(() => new FormularioRespondidoService(repositoryManager, logger, mapper));
            _cargoService = new Lazy<ICargoService>(() => new CargoService(repositoryManager, logger, mapper));
            _modalidadeService = new Lazy<IModalidadeService>(() => new ModalidadeService(repositoryManager, logger, mapper));
            _tipoVagaService = new Lazy<ITipoVagaService>(() => new TipoVagaService(repositoryManager, logger, mapper));
            _tipoCampoService = new Lazy<ITipoCampoService>(() => new TipoCampoService(repositoryManager, logger, mapper));
            _tipoFormularioService = new Lazy<ITipoFormularioService>(() => new TipoFormularioService(repositoryManager, logger, mapper));
            _estadoService = new Lazy<IEstadoService>(() => new EstadoService(repositoryManager, logger, mapper));
            _resultadoService = new Lazy<IResultadoService>(() => new ResultadoService(repositoryManager, logger, mapper));
            _campoService = new Lazy<ICampoService>(() => new CampoService(repositoryManager, logger, mapper));
            _formularioCampoService = new Lazy<IFormularioCampoService>(() => new FormularioCampoService(repositoryManager, logger, mapper));
            _candidaturaService = new Lazy<ICandidaturaService>(() => new CandidaturaService(repositoryManager, logger, mapper, _DocumentoService.Value));
            _entrevistaService = new Lazy<IEntrevistaService>(() => new EntrevistaService(repositoryManager, logger, mapper, _emailManagerService.Value));
            _funcionarioService = new Lazy<IFuncionarioService>(() => new FuncionarioService(repositoryManager, logger, mapper));
        }
        public ITemplateService TemplateService => _TemplateService.Value;
        public ITipoDocumentoService TipoDocumentoService => _TipoDocumentoService.Value;
        public IDocumentoService DocumentoService => _DocumentoService.Value;
        public IRecrutamentoService RecrutamentoService => _recrutamentoService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IVagaService VagaService => _vagaService.Value;
        public ICampoService CampoService => _campoService.Value;
		public IFormularioService FormularioService => _formularioService.Value;
        public ICargoService CargoService => _cargoService.Value;
        public ITipoVagaService TipoVagaService => _tipoVagaService.Value;
        public IModalidadeService ModalidadeService => _modalidadeService.Value;
        public ITipoCampoService TipoCampoService => _tipoCampoService.Value;
        public ITipoFormularioService TipoFormularioService => _tipoFormularioService.Value;
        public IFormularioCampoService FormularioCampoService => _formularioCampoService.Value;
        public ICandidaturaService CandidaturaService => _candidaturaService.Value;
        public IEstadoService EstadoService => _estadoService.Value;
        public IFormularioRespondidoService FormularioRespondidoService => _formularioRespondidoService.Value;
        public IEntrevistaService EntrevistaService => _entrevistaService.Value;
        public IResultadoService ResultadoService => _resultadoService.Value;
        public IFuncionarioService FuncionarioService => _funcionarioService.Value;
        public IEmpresaService EmpresaService => _empresaService.Value;
    }
}
