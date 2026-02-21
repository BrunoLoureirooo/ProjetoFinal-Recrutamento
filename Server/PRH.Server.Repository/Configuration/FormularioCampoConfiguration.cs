using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class FormularioCampoConfiguration : IEntityTypeConfiguration<FormularioCampo>
{
	internal static readonly Guid Formulario_Campo_DEMO_A = Guid.Parse("37CB687C-F15B-4A58-8065-2A4E1ED298DC");
	internal static readonly Guid Formulario_Campo_DEMO_B = Guid.Parse("8FE4996D-2391-4926-BA4D-5FA108924E0F");
	internal static readonly Guid Formulario_Campo_DEMO_C = Guid.Parse("4894eba1-14a5-4b90-b3d8-49ae0412eaec");
	internal static readonly Guid Formulario_Campo_DEMO_D = Guid.Parse("7db5701f-e2cd-49a4-9aaa-ba3baf3b5e39");
	internal static readonly Guid Formulario_Campo_DEMO_E = Guid.Parse("20459860-6dd8-43f6-98bc-73f251d4e421");
	internal static readonly Guid Formulario_Campo_DEMO_F = Guid.Parse("23267585-5c70-4dd1-8a4a-96261de2f57f");
	internal static readonly Guid Formulario_Campo_DEMO_G = Guid.Parse("baf89140-a4b2-424b-ad69-308c98067072");

	public void Configure(EntityTypeBuilder<FormularioCampo> builder)
	{
		builder.ToTable("FormularioCampo");

		builder.HasOne(x => x.Formulario)
			.WithMany(x => x.FormularioCampos)
			.HasForeignKey(x => x.FormularioId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(x => x.Campo)
			.WithMany(x => x.FormularioCampos)
			.HasForeignKey(x => x.CampoId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.Property(x => x.FormularioId).
			IsRequired();
		builder.Property(x => x.CampoId).
			IsRequired();
		builder.Property(x => x.Ativo).
			IsRequired();

        builder.HasData(
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_A,
                FormularioId = FormularioConfiguration.Formulario_DADOS_PESSOAIS,
				CampoId = CampoConfiguration.Campo_NOME ,
				Ativo= true
            },
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_B,
                FormularioId = FormularioConfiguration.Formulario_DADOS_PESSOAIS,
                CampoId = CampoConfiguration.Campo_EMAIL,
                Ativo = true
            },
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_C,
                FormularioId = FormularioConfiguration.Formulario_DADOS_PESSOAIS,
                CampoId = CampoConfiguration.Campo_DATA_NASCIMENTO,
                Ativo = true
            },
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_D,
                FormularioId = FormularioConfiguration.Formulario_DADOS_PESSOAIS,
                CampoId = CampoConfiguration.Campo_TELEFONE,
                Ativo = true
            },
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_E,
                FormularioId = FormularioConfiguration.Formulario_DADOS_PESSOAIS,
                CampoId = CampoConfiguration.Campo_MORADA,
                Ativo = true
            },
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_F,
                FormularioId = FormularioConfiguration.Formulario_CONTRATACAO,
                CampoId = CampoConfiguration.Campo_EXPERIENCIA,
                Ativo = true
            },
            new FormularioCampo
            {
                Id = Formulario_Campo_DEMO_G,
                FormularioId = FormularioConfiguration.Formulario_CONTRATACAO,
                CampoId = CampoConfiguration.Campo_CURRICULO,
                Ativo = true
            }
        );


    }
}