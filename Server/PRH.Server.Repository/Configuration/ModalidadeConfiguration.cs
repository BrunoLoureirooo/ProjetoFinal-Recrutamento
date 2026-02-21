using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class ModalidadeConfiguration : IEntityTypeConfiguration<Modalidade>
{
	internal static readonly Guid Tipo_Modalidade_PRESENCIAL = Guid.Parse("d5f20edc-3290-417f-b536-b5e053cdf376");
	internal static readonly Guid Tipo_Modalidade_VIRTUAL = Guid.Parse("3bdd48c4-ad73-4241-8823-17792de8f311");
	internal static readonly Guid Tipo_Modalidade_HIBRIDO = Guid.Parse("014533bb-205a-4f33-9ce3-2e7fc96dca99");

	public void Configure(EntityTypeBuilder<Modalidade> builder)
	{
		builder.ToTable("Lookups");

		builder.HasData(


		//Modalidade
		new Modalidade
		{
			Id = Tipo_Modalidade_PRESENCIAL,

			Descricao = "Presencial"

		},
		new Modalidade
		{
			Id = Tipo_Modalidade_VIRTUAL,

			Descricao = "Virtual"
		},
		new Modalidade
		{
			Id = Tipo_Modalidade_HIBRIDO,

			Descricao = "Hibrido"
		}
		);
	}
}