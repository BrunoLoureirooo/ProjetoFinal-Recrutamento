using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class FormularioRespondidoConfiguration : IEntityTypeConfiguration<FormularioRespondido>
{

    internal static readonly Guid Formulario_Respondido_DEMO_A = Guid.Parse("14ee9f25-03b1-4460-8bc6-84ae47fa8422");
    internal static readonly Guid Formulario_Respondido_DEMO_B = Guid.Parse("484157bd-2f6e-400e-93e2-c21018107514");
    internal static readonly Guid Formulario_Respondido_DEMO_C = Guid.Parse("8fafc343-e3a1-41fa-a70d-dd5f671beeaa");
    internal static readonly Guid Formulario_Respondido_DEMO_D = Guid.Parse("a43c2f48-664d-4a5b-b17f-bb36fdde71c4");
    internal static readonly Guid Formulario_Respondido_DEMO_E = Guid.Parse("7f3c9126-56d4-4cb5-82df-3a27fc8e064f");
    internal static readonly Guid Formulario_Respondido_DEMO_F = Guid.Parse("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79");

    public void Configure(EntityTypeBuilder<FormularioRespondido> builder)
    {
        builder.ToTable("FormularioRespondidos");

        builder.HasOne(x => x.Candidatura)
            .WithMany(x => x.FormularioRespondidos)
            .HasForeignKey(x => x.CandidaturaId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(x => x.Nome).
            HasMaxLength(100).
            IsRequired();
        builder.Property(x => x.Tipo).
            HasMaxLength(50).
            IsRequired();
        builder.Property(x => x.CandidaturaId).
            IsRequired();

        builder.HasData(

            new FormularioRespondido
            {
                Id = Formulario_Respondido_DEMO_A, 
                Nome = "Formulario de Contratação",
                Tipo = "Contratação",
                CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_A
            },
            new FormularioRespondido
            {
                Id = Formulario_Respondido_DEMO_B,
                Nome = "Formulario de Contratação",
                Tipo = "Contratação",
                CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_B
            },
            new FormularioRespondido
            {
                Id = Formulario_Respondido_DEMO_C,
                Nome = "Formulario de Contratação",
                Tipo = "Contratação",
                CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_C
            },
            new FormularioRespondido
            {
                Id = Formulario_Respondido_DEMO_D,
                Nome = "Dados Pessoais",
                Tipo = "Dados Pessoais",
                CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_C
            },
            new FormularioRespondido
            {
                Id = Formulario_Respondido_DEMO_E,
                Nome = "Formulario de Contratação",
                Tipo = "Contratação",
                CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_D
            },
            new FormularioRespondido
            {
                Id = Formulario_Respondido_DEMO_F,
                Nome = "Dados Pessoais",
                Tipo = "Dados Pessoais",
                CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_D
            }
        );
    }
}