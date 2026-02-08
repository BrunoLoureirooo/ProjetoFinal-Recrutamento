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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new EstabelecimentoConfiguration());
            modelBuilder.ApplyConfiguration(new DepartamentoConfiguration());
            modelBuilder.ApplyConfiguration(new EquipaConfiguration());
            modelBuilder.ApplyConfiguration(new FuncaoConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());
        }
        public DbSet<Empresa>? Empresas { get; set;}
        public DbSet<Estabelecimento>? Estabelecimentos { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }
        public DbSet<Equipa>? Equipas { get; set; }
        public DbSet<Funcao>? Funcoes { get; set; }
        public DbSet<Funcionario>? Funcionarios { get; set; }
    }
}
