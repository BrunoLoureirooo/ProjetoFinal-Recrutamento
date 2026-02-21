using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class CampoRespondidoConfiguration : IEntityTypeConfiguration<CampoRespondido>
{
    internal static readonly Guid Campo_Respondido_DEMO_A = Guid.Parse("fbe325e0-1ea4-4353-b435-b603e11c166f");
    internal static readonly Guid Campo_Respondido_DEMO_B = Guid.Parse("e6fd314e-094b-4985-89a2-a4353fb4367f");
    internal static readonly Guid Campo_Respondido_DEMO_C = Guid.Parse("5761aad9-1794-4bb4-8094-49b46dc4d028");
    internal static readonly Guid Campo_Respondido_DEMO_D = Guid.Parse("44fd408d-5ff3-4c4e-bdd1-c5d415592533");
    internal static readonly Guid Campo_Respondido_DEMO_E = Guid.Parse("a0923d06-5f13-4573-84f0-e3e280536036");
    internal static readonly Guid Campo_Respondido_DEMO_F = Guid.Parse("a3b0aab6-1c63-44be-84aa-8be4f04f8721");
    internal static readonly Guid Campo_Respondido_DEMO_G = Guid.Parse("764da4b2-4463-44e5-a076-4c74a7e81967");
    internal static readonly Guid Campo_Respondido_DEMO_H = Guid.Parse("7092f6a9-678a-4e4f-8fba-2934849c40d9");
    internal static readonly Guid Campo_Respondido_DEMO_I = Guid.Parse("af366e8c-3f71-4b3e-8316-e3bbcf4fe8e8");
    internal static readonly Guid Campo_Respondido_DEMO_J = Guid.Parse("0c1492d8-fc0d-446a-b13a-e11e9efdc0e2");
    internal static readonly Guid Campo_Respondido_DEMO_K = Guid.Parse("96788bfe-c6eb-4770-bfe2-843a1b6f5617");
    internal static readonly Guid Campo_Respondido_DEMO_L = Guid.Parse("0dfba58e-bf6e-4e36-a13f-8a759a07db3f");
    internal static readonly Guid Campo_Respondido_DEMO_M = Guid.Parse("a1fcd194-81e7-40e5-97f4-9859d899cbe4");
    internal static readonly Guid Campo_Respondido_DEMO_N = Guid.Parse("2e0c92b3-048e-460c-bc8e-16a0f93f7350");
    internal static readonly Guid Campo_Respondido_DEMO_O = Guid.Parse("e44c70e7-51d3-4c90-a80f-56f2cfcf24e0");
    internal static readonly Guid Campo_Respondido_DEMO_P = Guid.Parse("45e2e56b-9b17-49c7-8472-11fc70b29591");
    internal static readonly Guid Campo_Respondido_DEMO_Q = Guid.Parse("6c582d39-98ec-43d4-bd06-061674261f4d");
    internal static readonly Guid Campo_Respondido_DEMO_R = Guid.Parse("bf7d4145-7d93-4ec9-bd20-6b182a943228");

    public void Configure(EntityTypeBuilder<CampoRespondido> builder)
    {
        builder.ToTable("CampoRespondidos");

        builder.HasOne(x => x.FormularioRespondido)
            .WithMany(x => x.CampoRespondidos)
            .HasForeignKey(x => x.FormularioRespondidoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Nome).
            HasMaxLength(100).
            IsRequired();
        builder.Property(x => x.Resposta).
            HasMaxLength(1000).
            IsRequired();
        builder.Property(x => x.Tipo).
            HasMaxLength(20).
            IsRequired();
        builder.HasData(

            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_A,
                Nome = "Meses de Experiencia",
                Tipo = "int",
                Resposta = "92",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_A
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_B,
                Nome = "Curriculum Vitae",
                Tipo = "anexo",
                Resposta = "Anexo enviado!",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_A
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_C,
                Nome = "Meses de Experiencia",
                Tipo = "int",
                Resposta = "24",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_B
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_D,
                Nome = "Curriculum Vitae",
                Tipo = "anexo",
                Resposta = "Anexo enviado!",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_B
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_E,
                Nome = "Meses de Experiencia",
                Tipo = "int",
                Resposta = "48",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_C
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_F,
                Nome = "Curriculum Vitae",
                Tipo = "anexo",
                Resposta = "Anexo enviado!",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_C
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_G,
                Nome = "Nome Completo",
                Tipo = "string",
                Resposta = "Filipe Alberto Antunes Ribeiro",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_D
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_H,
                Nome = "Correio Eletronico",
                Tipo = "string",
                Resposta = "filipe.ribeiro@mail.pt",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_D
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_I,
                Nome = "Data de Nascimento",
                Tipo = "data",
                Resposta = "02-02-2000",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_D
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_J,
                Nome = "Contacto telefonico",
                Tipo = "número",
                Resposta = "917890123",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_D
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_K,
                Nome = "Morada de Residencia",
                Tipo = "string",
                Resposta = "Rua de Leiria, nº2, 2440-400, Leiria",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_D
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_L,
                Nome = "Meses de Experiencia",
                Tipo = "int",
                Resposta = "24",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_E
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_M,
                Nome = "Curriculum Vitae",
                Tipo = "anexo",
                Resposta = "Anexo enviado!",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_E
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_N,
                Nome = "Nome Completo",
                Tipo = "string",
                Resposta = "Ana Maria Antunes Martins",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_F
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_O,
                Nome = "Correio Eletronico",
                Tipo = "string",
                Resposta = "ana.martins@mail.pt",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_F
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_P,
                Nome = "Data de Nascimento",
                Tipo = "data",
                Resposta = "10-03-1998",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_F
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_Q,
                Nome = "Contacto telefonico",
                Tipo = "número",
                Resposta = "912345678",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_F
            },
            new CampoRespondido
            {
                Id = Campo_Respondido_DEMO_R,
                Nome = "Morada de Residencia",
                Tipo = "string",
                Resposta = "Rua de Leiria, nº3, 2440-400, Leiria",
                FormularioRespondidoId = FormularioRespondidoConfiguration.Formulario_Respondido_DEMO_F
            }
        );

    }
}