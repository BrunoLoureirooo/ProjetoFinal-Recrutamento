using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class CandidaturaConfiguration : IEntityTypeConfiguration<Candidatura>
{
	internal static readonly Guid Candidatura_DEMO_A = Guid.Parse("C373CD33-E77E-473C-8C0B-73AEB85955EF");
	internal static readonly Guid Candidatura_DEMO_B = Guid.Parse("F5D01A84-7B92-4B2F-ACBD-C74999E326F2");
	internal static readonly Guid Candidatura_DEMO_C = Guid.Parse("E7A9B382-218F-4267-8F19-DB9B6F564D92");
	internal static readonly Guid Candidatura_DEMO_D = Guid.Parse("3F71C2D1-66C5-464D-90D5-9DA11D02DC2C");

	public void Configure(EntityTypeBuilder<Candidatura> builder)
	{
		builder.ToTable("Candidaturas");

		builder.HasOne(x => x.Vaga)
			.WithMany(x => x.Candidaturas)
			.HasForeignKey(x => x.VagaId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(x => x.Estado)
			.WithMany(x => x.Candidaturas)
			.HasForeignKey(x => x.EstadoId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.Property(x => x.NIFcandidato).
			HasMaxLength(9).
			IsRequired();
		builder.Property(x => x.NomeCandidato).
			HasMaxLength(50);
		builder.Property(x => x.ContactoCandidato).
			HasMaxLength(9);
		builder.Property(x => x.EmailCandidato).
			HasMaxLength(100);
		builder.Property(x => x.EstadoId).
			IsRequired();

		builder.HasData(
			new Candidatura
			{
				Id = Candidatura_DEMO_A,
				VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_SOFTWARE,
				NIFcandidato = 255101791,
				NomeCandidato = "Manuel Da Silva",
				ContactoCandidato = 910582225,
				EmailCandidato = "manuel.silva@mail.pt",
				EstadoId = EstadoConfiguration.Tipo_Estado_Decorrer
			},
			new Candidatura
			{
				Id = Candidatura_DEMO_B,
				VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_SOFTWARE,
				NIFcandidato = 219324964,
				NomeCandidato = "José Ferreira",
				ContactoCandidato = 913456789,
				EmailCandidato = "jose.ferreira@mail.pt",
				EstadoId = EstadoConfiguration.Tipo_Estado_Decorrer
            },
			new Candidatura
			{
				Id = Candidatura_DEMO_C,
				VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_COMERCIAL,
				NIFcandidato = 224977270,
				NomeCandidato = "Filipe Ribeiro",
				ContactoCandidato = 917890123,
				EmailCandidato = "filipe.ribeiro@mail.pt",
				EstadoId = EstadoConfiguration.Tipo_Estado_Decorrer
            },
			new Candidatura
			{
				Id = Candidatura_DEMO_D,
				VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_COMERCIAL,
				NIFcandidato = 220913315,
				NomeCandidato = "Ana Martins",
				ContactoCandidato = 912345678,
				EmailCandidato = "ana.martins@mail.pt",
				EstadoId = EstadoConfiguration.Tipo_Estado_Decorrer
            }
		);
	}
}