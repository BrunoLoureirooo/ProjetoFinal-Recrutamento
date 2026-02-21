using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class CampoConfiguration : IEntityTypeConfiguration<Campo>
{
	internal static readonly Guid Campo_NOME = Guid.Parse("9DE6573F-BEBD-4277-A43F-02C74737EC9E");
	internal static readonly Guid Campo_EMAIL = Guid.Parse("2699D419-F8F2-4862-B0E6-ADD5469C3B48");
	internal static readonly Guid Campo_DATA_NASCIMENTO = Guid.Parse("d3a9f7c2-5b4e-41d8-b0f6-8e1c7a52d9f3"); 
	internal static readonly Guid Campo_TELEFONE = Guid.Parse("e2f1c7d5-3b4e-41d8-b0f6-8e1c7a52d9f4");
	internal static readonly Guid Campo_MORADA = Guid.Parse("f3a2b6c4-4d3e-48d6-a0f8-7e1c2a59d3b5");
	internal static readonly Guid Campo_CURRICULO = Guid.Parse("b4c7d2a5-5e3f-49d8-b1f7-9e2c8a62e4d6");
	internal static readonly Guid Campo_EXPERIENCIA = Guid.Parse("c5d8a4b3-6f2e-41d9-c0e5-6b3c7d52a9e7");
	internal static readonly Guid Campo_HABILIDADES = Guid.Parse("d6e9b5c2-7a4f-48d1-d0e8-5c4b8a63f1e8");



	public void Configure(EntityTypeBuilder<Campo> builder)
	{
		builder.ToTable("Campos");

		builder.HasOne(x => x.TipoCampo)
			.WithMany(x => x.Campos)
			.HasForeignKey(x => x.TipoId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.Property(x => x.Nome).
			HasMaxLength(100).
			IsRequired();
		builder.Property(x => x.NomeUnico).
			HasMaxLength(100).
			IsRequired();
		builder.Property(x => x.TipoId).
			IsRequired();
		builder.Property(x => x.IsOpcional).
			IsRequired();

        builder.HasIndex(x => x.NomeUnico)
			.IsUnique();

        builder.HasData(


        //Formularios
        new Campo
        {
            Id = Campo_NOME,
            Nome = "Nome Completo",
            NomeUnico = "NOME_COMPLETO",
            IsOpcional = false,
            TipoId = TipoCampoConfiguration.Tipo_Campo_STRING

        },
        new Campo
        {
            Id = Campo_EMAIL,
            Nome = "Correio Eletronico",
            NomeUnico = "CORREIO_ELETRONICO",
            IsOpcional = false,
            TipoId = TipoCampoConfiguration.Tipo_Campo_STRING
        },
        new Campo
        {
            Id = Campo_DATA_NASCIMENTO,
            Nome = "Data de Nascimento",
            NomeUnico = "DATA_DE_NASCIMENTO",
            IsOpcional = true,
            TipoId = TipoCampoConfiguration.Tipo_Campo_DATA
        },
        new Campo
        {
            Id = Campo_TELEFONE,
            Nome = "Contacto telefonico",
            NomeUnico = "CONTACTO_TELEFONICO",
            IsOpcional = false,
            TipoId = TipoCampoConfiguration.Tipo_Campo_NUMERO
        },
        new Campo
        {
            Id = Campo_MORADA,
            Nome = "Morada de Residencia",
            NomeUnico = "MORADA_DE_RESIDENCIA",
            IsOpcional = true,
            TipoId = TipoCampoConfiguration.Tipo_Campo_STRING
        },
        new Campo
        {
            Id = Campo_EXPERIENCIA,
            Nome = "Meses de Experiencia",
            NomeUnico = "MESES_DE_EXPERIENCIA",
            IsOpcional = true,
            TipoId = TipoCampoConfiguration.Tipo_Campo_NUMERO
        },
        new Campo
        {
            Id = Campo_CURRICULO,
            Nome = "Curriculum Vitae",
            NomeUnico = "CV",
            IsOpcional = false,
            TipoId = TipoCampoConfiguration.Tipo_Campo_ANEXO
        }
        );


    }
}