using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
{
	internal static readonly Guid Cargo_Porteiro = Guid.Parse("c3d91f85-7e42-4a6b-b2d0-8f1e37ac59b4");
	internal static readonly Guid Cargo_Engenheiro_Software = Guid.Parse("5bbef10e-f9e6-45ad-abcb-bc63bbf13fbe");
	internal static readonly Guid Cargo_Analista_Dados = Guid.Parse("f0d844f3-8c29-4c6d-8042-9473ec8191aa");
	internal static readonly Guid Cargo_Contador = Guid.Parse("66a77ab9-a4d7-4c11-9b2d-928782b3bd95");
	internal static readonly Guid Cargo_Assistente_Administrativo = Guid.Parse("77552b58-f64b-4f22-8197-cbffd2aa7bfa");
	internal static readonly Guid Cargo_Especialista_RH = Guid.Parse("7f10ab2e-a96c-4bbb-9ebc-c8a2b5811b89");
	internal static readonly Guid Cargo_Recrutador = Guid.Parse("67dbb27d-53d7-4962-ab44-8f7a2c23a301");
	internal static readonly Guid Cargo_Atendente = Guid.Parse("5251ef65-3843-461e-a0fc-417e27cd6c0f");
	internal static readonly Guid Cargo_Tecnico_Manutencao = Guid.Parse("ffed9c07-3294-4b32-8c21-2de2bf2dbd32");
    internal static readonly Guid Cargo_Comercial = Guid.Parse("dc8faf27-b7b3-406e-82c1-ca1f9339a7ea");

    public void Configure(EntityTypeBuilder<Cargo> builder)
	{
		builder.ToTable("Cargos");

		builder.Property(x => x.Nome).
			HasMaxLength(100).
			IsRequired();

		builder.HasData(
			new Cargo{Id = Cargo_Porteiro,Nome = "Porteiro"},
			new Cargo { Id = Cargo_Engenheiro_Software, Nome = "Engenheiro de Software" },
			new Cargo { Id = Cargo_Analista_Dados, Nome = "Analista de Dados" },
			new Cargo { Id = Cargo_Contador, Nome = "Contador" },
			new Cargo { Id = Cargo_Assistente_Administrativo, Nome = "Assistente Administrativo" },
			new Cargo { Id = Cargo_Especialista_RH, Nome = "Especialista em Recursos Humanos" },
			new Cargo { Id = Cargo_Recrutador, Nome = "Recrutador" },
			new Cargo { Id = Cargo_Atendente, Nome = "Atendente" },
			new Cargo { Id = Cargo_Tecnico_Manutencao, Nome = "Técnico de Manutenção" },
			new Cargo { Id = Cargo_Comercial, Nome = "Comercial de Vendas"}
		);
	}
}