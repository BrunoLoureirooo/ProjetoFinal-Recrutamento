using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class TipoVagaConfiguration : IEntityTypeConfiguration<TipoVaga>
{
	internal static readonly Guid Tipo_Vaga_PART_TIME = Guid.Parse("d5f1a8c3-79e2-4b0d-b6f7-91c4de38fa25");
	internal static readonly Guid Tipo_Vaga_FULL_TIME = Guid.Parse("b91e4d76-2c3a-45f8-ae7d-58f2b0d9c184");

	public void Configure(EntityTypeBuilder<TipoVaga> builder)
	{
		builder.ToTable("Lookups");

		builder.HasData(


		//Vaga
		new TipoVaga
		{
			Id = Tipo_Vaga_PART_TIME,

			Descricao = "Part-Time"

		},
		new TipoVaga
		{
			Id = Tipo_Vaga_FULL_TIME,

			Descricao = "Full-Time"
		}
		);
	}
}