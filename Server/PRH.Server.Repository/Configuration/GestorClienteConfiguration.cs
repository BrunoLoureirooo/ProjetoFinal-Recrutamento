using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration;

public class GestorClienteConfiguration : IEntityTypeConfiguration<GestorCliente>
{

	internal static readonly Guid GESTOR_TRIGENIUS = Guid.Parse("b7f1dc79-3f3d-4d42-a6a4-d27e015f1d73");

	public void Configure(EntityTypeBuilder<GestorCliente> builder)
    {
        builder.ToTable("GestorClientes");

        builder.HasOne(x => x.Empresa)
            .WithOne(x => x.Gestor)
            .HasForeignKey<GestorCliente>(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new GestorCliente
        {
            Id = GESTOR_TRIGENIUS,
            EmpresaId = Empresa_TRIGENIUS,
            ApplicationUserId = UserConfiguration.GESTOR_TRIGENIUS.ToString()
        });
    }
}