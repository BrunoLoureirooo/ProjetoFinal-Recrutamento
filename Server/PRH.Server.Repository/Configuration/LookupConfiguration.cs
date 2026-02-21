using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
{
	public void Configure(EntityTypeBuilder<Lookup> builder)
	{
		builder.ToTable("Lookups");

		builder.HasDiscriminator(x => x.Tipo)
			.HasValue<TipoFormulario>("Formulario")
			.HasValue<TipoCampo>("Campo")
			.HasValue<TipoVaga>("Vaga")
			.HasValue<Modalidade>("Modalidade")
			.HasValue<Estado>("Estado"); ;

		builder.Property(x => x.Descricao).
			HasMaxLength(50).
			IsRequired();

	}
}