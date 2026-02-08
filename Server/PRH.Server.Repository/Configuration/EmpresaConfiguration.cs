using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        internal static readonly Guid EMPRESA_DEMO_A = Guid.Parse("bd00f150-16ea-48f1-b85e-cda3af329dac");
        internal static readonly Guid EMPRESA_DEMO_B = Guid.Parse("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae");

        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");
            
            builder.HasData(
            new Empresa
            {
                Id = EMPRESA_DEMO_A,
                Nome = "DEMO A",
            },
            new Empresa
            {
                Id = EMPRESA_DEMO_B,
                Nome = "DEMO B",
            }
            );
        }
    }
}
