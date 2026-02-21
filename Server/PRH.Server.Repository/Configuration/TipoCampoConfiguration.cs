using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class TipoCampoConfiguration : IEntityTypeConfiguration<TipoCampo>
{

	internal static readonly Guid Tipo_Campo_STRING = Guid.Parse("a7d2f9c5-3b4e-41d8-b0f6-8e1c7a52d9f3");
	internal static readonly Guid Tipo_Campo_ANEXO = Guid.Parse("c9f7a5d2-4b3e-48d6-a0f8-7e1c2a59d3b4");
	internal static readonly Guid Tipo_Campo_NUMERO = Guid.Parse("e7f2a3d6-8b4c-49d7-e0f9-4d5c9b74e2c9");
	internal static readonly Guid Tipo_Campo_DATA = Guid.Parse("f8a3b2d7-9c5e-48d7-f1e0-3d6c9b85e4d1");
	internal static readonly Guid Tipo_Campo_BOOL = Guid.Parse("b9b4c3d8-7a6f-41d9-a0e5-6b4c8d75f2e3");
	internal static readonly Guid Tipo_Campo_LOOKUP = Guid.Parse("c1c5d6e7-8f9a-42b1-b0d2-7a8b9c0d1e2f");


	public void Configure(EntityTypeBuilder<TipoCampo> builder)
	{
		builder.ToTable("Lookups");

		builder.HasData(


		//Campos
		new TipoCampo
		{
			Id = Tipo_Campo_STRING,

			Descricao = "string"
		},
		new TipoCampo
		{
			Id = Tipo_Campo_ANEXO,

			Descricao = "anexo"
		},
		new TipoCampo
		{
			Id = Tipo_Campo_NUMERO,

			Descricao = "número"
		},
		new TipoCampo
		{
			Id = Tipo_Campo_DATA,

			Descricao = "data"
		},
		new TipoCampo
		{
			Id = Tipo_Campo_BOOL,

			Descricao = "booleano"
		},
		new TipoCampo
		{
			Id = Tipo_Campo_LOOKUP,

			Descricao = "lookup"
		}
		);
	}
}