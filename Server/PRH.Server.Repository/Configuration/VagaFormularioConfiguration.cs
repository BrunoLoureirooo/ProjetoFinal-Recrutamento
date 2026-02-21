using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class VagaFormularioConfiguration : IEntityTypeConfiguration<VagaFormulario>
{
	//VagaFormulario
	internal static readonly Guid Vaga_Formulario_DEMO_A = Guid.Parse("d5fd6166-b990-4c8e-8747-f033563d1fb1");
	internal static readonly Guid Vaga_Formulario_DEMO_B = Guid.Parse("f8414f9e-1ab6-4262-8331-8343b73a49a7");
    internal static readonly Guid Vaga_Formulario_DEMO_C = Guid.Parse("3e1a9c4f-bc98-4a27-9d9b-1c8f8ea3b582");

    public void Configure(EntityTypeBuilder<VagaFormulario> builder)
	{
		builder.ToTable("VagaFormularios");

		builder.HasOne(x => x.Formulario)
			.WithMany(x => x.VagaFormularios)
			.HasForeignKey(x => x.FormularioId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(x => x.Vaga)
			.WithMany(x => x.VagasFormularios)
			.HasForeignKey(x => x.VagaId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasData(
			new VagaFormulario { 
				Id = Vaga_Formulario_DEMO_A, 
				VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_SOFTWARE, 
				FormularioId = FormularioConfiguration.Formulario_CONTRATACAO 
			},
			new VagaFormulario { 
				Id = Vaga_Formulario_DEMO_B, 
				VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_COMERCIAL, 
				FormularioId = FormularioConfiguration.Formulario_CONTRATACAO 
			},
            new VagaFormulario
            {
                Id = Vaga_Formulario_DEMO_C,
                VagaId = VagaConfiguration.Vaga_DEMO_TRIGENIUS_COMERCIAL,
                FormularioId = FormularioConfiguration.Formulario_DADOS_PESSOAIS
            }
        );
	}
}