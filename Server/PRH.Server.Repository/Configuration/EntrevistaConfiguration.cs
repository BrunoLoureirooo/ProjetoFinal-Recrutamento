using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;


namespace PRH.Server.Repository.Configuration;
public class EntrevistaConfiguration : IEntityTypeConfiguration<Entrevista>
{
	internal static readonly Guid ENTREVISTA_DEMO_A = Guid.Parse("f7a2c9d4-3b5e-41d8-b0f6-8e1d7a52c9f3");
	internal static readonly Guid ENTREVISTA_DEMO_B = Guid.Parse("5a5b435c-91af-4d85-a36a-dee386a272c8");
	internal static readonly Guid ENTREVISTA_DEMO_C = Guid.Parse("e6c52afb-7368-469a-a130-6faa8875a533");
	internal static readonly Guid ENTREVISTA_DEMO_D = Guid.Parse("913eb7f7-3fd4-4e62-889e-5c174ca4eeaf");

	public void Configure(EntityTypeBuilder<Entrevista> builder)
	{
		builder.ToTable("Entrevistas");

		builder.HasOne(x => x.Resultado)
			.WithMany(x => x.Entrevistas)
			.HasForeignKey(x => x.ResultadoEntrevistaId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.Property(x => x.CandidaturaId).
			IsRequired();
		builder.Property(x => x.EntrevistadorId).
			IsRequired();
		builder.Property(x => x.DataHora).
			IsRequired();
		builder.Property(x => x.Notas).
			HasMaxLength(1000);

		builder.HasData(
			new Entrevista { 
				Id = ENTREVISTA_DEMO_A,  
				CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_A, 
				EntrevistadorId = FuncionarioConfiguration.FUNCIONARIO_DEMO_A, 
				DataHora = DateTime.Parse("2025-08-20 14:30:00"), 
				Notas = "", 
				ResultadoEntrevistaId = ResultadoConfiguration.Tipo_Resultado_Vazio 
			},
			new Entrevista {
				Id = ENTREVISTA_DEMO_B,
				CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_B,
				EntrevistadorId = FuncionarioConfiguration.FUNCIONARIO_DEMO_B,
				DataHora = DateTime.Parse("2025-03-29 10:00:00"),
				Notas = "Precisa melhorar na comunicação.",
				ResultadoEntrevistaId = ResultadoConfiguration.Tipo_Resultado_Proxima_Fase 
			},
			new Entrevista { 
				Id = ENTREVISTA_DEMO_C,  
				CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_C, 
				EntrevistadorId = FuncionarioConfiguration.FUNCIONARIO_DEMO_C, 
				DataHora = DateTime.Parse("2025-03-30 09:45:00"), 
				Notas = "Bom desempenho técnico.", 
				ResultadoEntrevistaId = ResultadoConfiguration.Tipo_Resultado_Aprovado 
			},
			new Entrevista { 
				Id = ENTREVISTA_DEMO_D,  
				CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_D, 
				EntrevistadorId = FuncionarioConfiguration.FUNCIONARIO_DEMO_D, 
				DataHora = DateTime.Parse("2025-04-01 15:20:00"), 
				Notas = "Não atendeu aos requisitos mínimos.", 
				ResultadoEntrevistaId = ResultadoConfiguration.Tipo_Resultado_Reprovado }
		);
	}
}