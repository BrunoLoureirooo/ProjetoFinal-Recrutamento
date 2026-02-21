using PRH.Server.Repository.Contracts;
using PRH.Server.Repository.Contracts.Lookups;
using PRH.Server.Repository.Lookups;

namespace PRH.Server.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private readonly Lazy<ITemplateRepository> _TemplateRepository;
        private readonly Lazy<ITipoDocumentoRepository> _TipoDocumentoRepository;
        private readonly Lazy<IDocumentoRepository> _DocumentoRepository;
        private readonly Lazy<IRecrutamentoRepository> _recrutamentoRepository;
        private readonly Lazy<IFormularioRepository> _formularioRepository;
        private readonly Lazy<IFormularioRespondidoRepository> _formularioRespondidoRepository;
        private readonly Lazy<IVagaRepository> _vagaRepository;
        private readonly Lazy<IVagaFormularioRepository> _vagaFormularioRepository;
        private readonly Lazy<ICargoRepository> _cargoRepository;
        private readonly Lazy<IModalidadeRepository> _modalidadeRepository;
        private readonly Lazy<ITipoVagaRepository> _tipoVagaRepository;
        private readonly Lazy<ITipoCampoRepository> _tipoCampoRepository;
        private readonly Lazy<ITipoFormularioRepository> _tipoFormularioRepository;
        private readonly Lazy<IEstadoRepository> _estadoRepository;
        private readonly Lazy<IResultadoRepository> _resultadoRepository;
        private readonly Lazy<ICampoRepository> _campoRepository;
        private readonly Lazy<IFormularioCampoRepository> _formularioCampoRepository;
        private readonly Lazy<ICandidaturaRepository> _candidaturaRepository;
        private readonly Lazy<IEntrevistaRepository> _entrevistaRepository;
        private readonly Lazy<IFuncionarioRepository> _funcionarioRepository;
        private readonly Lazy<IGestorClienteRepository> _gestorClienteRepository;
        private readonly Lazy<IEmpresaRepository> _empresaRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _empresaRepository = new Lazy<IEmpresaRepository>(() => new EmpresaRepository(repositoryContext));
            _TemplateRepository = new Lazy<ITemplateRepository>(() => new TemplateRepository(repositoryContext));
            _TipoDocumentoRepository = new Lazy<ITipoDocumentoRepository>(() => new TipoDocumentoRepository(repositoryContext));
            _DocumentoRepository = new Lazy<IDocumentoRepository>(() => new DocumentoRepository(repositoryContext));
            _recrutamentoRepository = new Lazy<IRecrutamentoRepository>(() => new RecrutamentoRepository(repositoryContext));
            _vagaRepository = new Lazy<IVagaRepository>(() => new VagaRepository(repositoryContext));
            _vagaFormularioRepository = new Lazy<IVagaFormularioRepository>(() => new VagaFormularioRepository(repositoryContext));
            _formularioRepository = new Lazy<IFormularioRepository>(() => new FormularioRepository(repositoryContext));
            _formularioRespondidoRepository = new Lazy<IFormularioRespondidoRepository>(() => new FormularioRespondidoRepository(repositoryContext));
            _candidaturaRepository = new Lazy<ICandidaturaRepository>(() => new CandidaturaRepository(repositoryContext));
            _entrevistaRepository = new Lazy<IEntrevistaRepository>(() => new EntrevistaRepository(repositoryContext));
            _funcionarioRepository = new Lazy<IFuncionarioRepository>(() => new FuncionarioRepository(repositoryContext));
            _gestorClienteRepository = new Lazy<IGestorClienteRepository>(() => new GestorClienteRepository(repositoryContext));

            _cargoRepository = new Lazy<ICargoRepository>(() => new CargoRepository(repositoryContext));
            _modalidadeRepository = new Lazy<IModalidadeRepository>(() => new ModalidadeRepository(repositoryContext));
            _tipoVagaRepository = new Lazy<ITipoVagaRepository>(() => new TipoVagaRepository(repositoryContext));
            _tipoCampoRepository = new Lazy<ITipoCampoRepository>(() => new TipoCampoRepository(repositoryContext));
            _tipoFormularioRepository = new Lazy<ITipoFormularioRepository>(() => new TipoFormularioRepository(repositoryContext));
            _estadoRepository = new Lazy<IEstadoRepository>(() => new EstadoRepository(repositoryContext));
            _resultadoRepository = new Lazy<IResultadoRepository>(() => new ResultadoRepository(repositoryContext));

            _campoRepository = new Lazy<ICampoRepository>(() => new CampoRepository(repositoryContext));
            _formularioCampoRepository = new Lazy<IFormularioCampoRepository>(() => new FormularioCampoRepository(repositoryContext));
        }
        public ITemplateRepository Template => _TemplateRepository.Value;
        public ITipoDocumentoRepository TipoDocumento => _TipoDocumentoRepository.Value;
        public IDocumentoRepository Documento => _DocumentoRepository.Value;

        public IRecrutamentoRepository Recrutamento => _recrutamentoRepository.Value;

        public IVagaRepository Vaga => _vagaRepository.Value;

        public IFormularioRepository Formulario => _formularioRepository.Value;
        public ICampoRepository Campo => _campoRepository.Value;

        public ICargoRepository Cargo => _cargoRepository.Value;

        public ITipoVagaRepository TipoVaga => _tipoVagaRepository.Value;

        public IModalidadeRepository Modalidade => _modalidadeRepository.Value;

        public ITipoCampoRepository TipoCampo => _tipoCampoRepository.Value;

        public ITipoFormularioRepository TipoFormulario => _tipoFormularioRepository.Value;

        public IFormularioCampoRepository FormularioCampo => _formularioCampoRepository.Value;

        public ICandidaturaRepository Candidatura => _candidaturaRepository.Value;

        public IEstadoRepository Estado => _estadoRepository.Value;

        public IFormularioRespondidoRepository FormularioRespondido => _formularioRespondidoRepository.Value;

        public IEntrevistaRepository Entrevista => _entrevistaRepository.Value;

        public IResultadoRepository Resultado => _resultadoRepository.Value;

        public IFuncionarioRepository Funcionario => _funcionarioRepository.Value;

        public IGestorClienteRepository GestorCliente => _gestorClienteRepository.Value;

        public IVagaFormularioRepository VagaFormulario => _vagaFormularioRepository.Value;

        public IEmpresaRepository Empresa => _empresaRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }

}
