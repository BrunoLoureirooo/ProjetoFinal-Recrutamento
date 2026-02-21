using PRH.Server.Entities.Models;
using PRH.Server.Repository.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PRH.Server.Repository
{
    public class RepositoryContext : IdentityDbContext<ApplicationUser>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new EstabelecimentoConfiguration());
            modelBuilder.ApplyConfiguration(new DepartamentoConfiguration());
            modelBuilder.ApplyConfiguration(new EquipaConfiguration());
            modelBuilder.ApplyConfiguration(new FuncaoConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());
            modelBuilder.ApplyConfiguration(new GestorClienteConfiguration());

            modelBuilder.ApplyConfiguration(new CampoConfiguration());
            modelBuilder.ApplyConfiguration(new FormularioConfiguration());
			modelBuilder.ApplyConfiguration(new FormularioCampoConfiguration());
			modelBuilder.ApplyConfiguration(new RecrutamentoConfiguration());
			modelBuilder.ApplyConfiguration(new CargoConfiguration());
			modelBuilder.ApplyConfiguration(new VagaConfiguration());
			modelBuilder.ApplyConfiguration(new CandidaturaConfiguration());
			modelBuilder.ApplyConfiguration(new VagaFormularioConfiguration());
			modelBuilder.ApplyConfiguration(new DocumentosNecessariosConfiguration());
			modelBuilder.ApplyConfiguration(new EntrevistaConfiguration());
			modelBuilder.ApplyConfiguration(new TipoDocumentoConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentoConfiguration());

			modelBuilder.ApplyConfiguration(new LookupConfiguration());
			modelBuilder.ApplyConfiguration(new TipoFormularioConfiguration());
			modelBuilder.ApplyConfiguration(new TipoCampoConfiguration());
			modelBuilder.ApplyConfiguration(new TipoVagaConfiguration());
			modelBuilder.ApplyConfiguration(new ModalidadeConfiguration());
			modelBuilder.ApplyConfiguration(new ResultadoConfiguration());
			modelBuilder.ApplyConfiguration(new EstadoConfiguration());
			modelBuilder.ApplyConfiguration(new EmpresaTipoDocumentoConfiguration());

			modelBuilder.ApplyConfiguration(new CampoRespondidoConfiguration());
			modelBuilder.ApplyConfiguration(new FormularioRespondidoConfiguration());
			modelBuilder.ApplyConfiguration(new OpcoesCampoLookupConfiguration());

            base.OnModelCreating(modelBuilder);

		}
        public DbSet<Empresa>? Empresas { get; set;}
        public DbSet<Estabelecimento>? Estabelecimentos { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }
        public DbSet<Equipa>? Equipas { get; set; }
        public DbSet<Funcao>? Funcoes { get; set; }
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<GestorCliente>? GestorClientes { get; set; }
        public DbSet<Campo>? Campos { get; set; }
        public DbSet<Candidatura>? Candidaturas { get; set; }
        public DbSet<Cargo>? Cargos { get; set; }
        public DbSet<Documento>? Documentos { get; set; }
		public DbSet<DocumentosNecessarios>? DocumentosNecessarios { get; set; }
		public DbSet<Entrevista>? Entrevistas { get; set; }
		public DbSet<Estado>? Estados { get; set; }
		public DbSet<Formulario>? Formularios { get; set; }
		public DbSet<FormularioCampo>? FormularioCampos { get; set; }
		public DbSet<Lookup>? Lookups { get; set; }
		public DbSet<Recrutamento>? Recrutamentos { get; set; }
		public DbSet<Resultado>? Resultados { get; set; }
		public DbSet<Template>? Templates { get; set; }
		public DbSet<TipoCampo>? TipoCampos { get; set; }
		public DbSet<TipoDocumento>? TipoDocumentos { get; set; }
		public DbSet<TipoFormulario>? TipoFormularios { get; set; }
		public DbSet<TipoVaga>? TipoVagas { get; set; }
		public DbSet<Vaga>? Vagas { get; set; }
		public DbSet<VagaFormulario>? VagaFormularios { get; set; }
		public DbSet<EmpresaTipoDocumento>? EmpresaTipoDocumentos { get; set;}
		public DbSet<Modalidade>? Modalidades { get; set; }
		public DbSet<CampoRespondido>? CampoRespondidos { get; set; }
		public DbSet<FormularioRespondido>? FormularioRespondidos { get; set; }
		public DbSet<OpcoesCampoLookup>? OpcoesCampoLookups { get; set; }

	}
}
