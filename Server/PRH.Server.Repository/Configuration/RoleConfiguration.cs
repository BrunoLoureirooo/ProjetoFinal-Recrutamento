using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PRH.Server.Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        internal static readonly Guid ROLE_ADMINISTRADOR = Guid.Parse("418b0ac3-4dba-4ccf-883e-63fdd7ac62a9");
        internal static readonly Guid ROLE_FUNCIONARIO = Guid.Parse("52c71224-fe99-4ec0-9130-0f0de200eec9");
        internal static readonly Guid ROLE_GESTOR_CLIENTE = Guid.Parse("a3e57c14-2b8f-4e9f-9d4a-7a2b172b0e48");

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Id = ROLE_ADMINISTRADOR.ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole
            {
                Id = ROLE_FUNCIONARIO.ToString(),
                Name = "Funcionario",
                NormalizedName = "FUNCIONARIO"
            },
            new IdentityRole
            {
                Id = ROLE_GESTOR_CLIENTE.ToString(),
                Name = "GestorCliente",
                NormalizedName = "GESTORCLIENTE"
            }
            );
        }
    }
}
