using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class FormularioConfiguration : IEntityTypeConfiguration<Formulario>
{
    internal static readonly Guid Formulario_CONTRATACAO = Guid.Parse("52083369-C174-469B-A7ED-844C23BBD948");
    internal static readonly Guid Formulario_DADOS_PESSOAIS = Guid.Parse("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4");

    public void Configure(EntityTypeBuilder<Formulario> builder)
    {
        builder.ToTable("Formularios");

        builder.HasOne(x => x.Tipo)
            .WithMany(x => x.Formularios)
            .HasForeignKey(x => x.TipoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.NomeUnico)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.TipoId)
            .IsRequired();

        builder.HasIndex(x => x.NomeUnico)
            .IsUnique();

        builder.HasData(
            new Formulario
            {
                Id = Formulario_CONTRATACAO,
                Nome = "Formulario de Contratação",
                NomeUnico = "FORMULARIO_DE_CONTRATACAO",
                TipoId = TipoFormularioConfiguration.Tipo_Formulario_CONTRATACAO
            },
            new Formulario
            {
                Id = Formulario_DADOS_PESSOAIS,
                Nome = "Dados Pessoais",
                NomeUnico = "DADOS_PESSOAIS",
                TipoId = TipoFormularioConfiguration.Tipo_Formulario_DADOS_PESSOAIS
            }
        );
    }
}
