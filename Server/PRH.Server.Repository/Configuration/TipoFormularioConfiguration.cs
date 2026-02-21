using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class TipoFormularioConfiguration : IEntityTypeConfiguration<TipoFormulario>
{

	internal static readonly Guid Tipo_Formulario_CONTRATACAO = Guid.Parse("f3a7d9c2-5b4e-41d8-b0f6-8e1c7a52d9f3");
	internal static readonly Guid Tipo_Formulario_DADOS_PESSOAIS = Guid.Parse("d9c7a5f2-3b4e-48f6-a0d8-7f1e2c4a19b3");
	internal static readonly Guid Tipo_Formulario_EXPERIENCIA = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
	internal static readonly Guid Tipo_Formulario_ANEXOS = Guid.Parse("b2c3d4e5-f678-90ab-cdef-234567890123");


	public void Configure(EntityTypeBuilder<TipoFormulario> builder)
	{
		builder.ToTable("Lookups");

		builder.HasData(
		
		//Formularios
		new TipoFormulario
		{
			Id = Tipo_Formulario_CONTRATACAO,
			Descricao = "Contratação"
		},
		new TipoFormulario
		{
			Id = Tipo_Formulario_DADOS_PESSOAIS,
			Descricao = "Dados Pessoais"
		},
		new TipoFormulario
		{
			Id = Tipo_Formulario_EXPERIENCIA,
			Descricao = "Experiencia Profissional"
		},
		new TipoFormulario
		{
			Id = Tipo_Formulario_ANEXOS,
			Descricao = "Anexos"
		}
		);
	}
}