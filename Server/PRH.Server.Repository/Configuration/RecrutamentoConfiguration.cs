using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class RecrutamentoConfiguration : IEntityTypeConfiguration<Recrutamento>
{
	//recrutamento
	internal static readonly Guid Recrutamento_DEMO_TRIGENIUS_SOFTWARE = Guid.Parse("a3c9e742-5fbd-4d7e-9c12-ef8a6b1d34f7");
	internal static readonly Guid Recrutamento_DEMO_TRIGENIUS_COMERCIAL = Guid.Parse("f28d7c91-3bae-45e6-a9f4-7cde812b65a3");



	public void Configure(EntityTypeBuilder<Recrutamento> builder)
	{
		builder.ToTable("Recrutamentos");

		builder.HasOne(x => x.Empresa)
			.WithMany(x => x.Recrutamentos)
			.HasForeignKey(x => x.EmpresaID)
			.OnDelete(DeleteBehavior.Cascade);

		
		builder.Property(x => x.EmpresaID).
			IsRequired();
		builder.Property(x => x.Data).
			IsRequired();

		builder.HasData(
				// Recrutamentos para Empresa TECH INNOVATORS
				new Recrutamento
				{
					Id = Recrutamento_DEMO_TRIGENIUS_SOFTWARE,
					Nome = "Recrutamento Engenheiro de Software Trigenius",
					Data = DateTime.Parse("2025-03-01"),
					EmpresaID = EmpresaConfiguration.Empresa_TRIGENIUS
                },
				new Recrutamento
				{
					Id = Recrutamento_DEMO_TRIGENIUS_COMERCIAL,
					Nome = "Recrutamento Comercial Trigenius",
					Data = DateTime.Parse("2025-03-10"),
					EmpresaID = EmpresaConfiguration.Empresa_TRIGENIUS
                }
			);
	}
}