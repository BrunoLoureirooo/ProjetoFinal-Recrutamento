using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    internal static readonly Guid EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("9fc5fb53-6945-4587-98bc-ef4bb4926c5e");
    internal static readonly Guid EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL = Guid.Parse("88754e7b-3fd7-4959-98d8-547aa2eb3da3");
    internal static readonly Guid EMPRESA_DEMO_A_DIRECAO = Guid.Parse("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41");

    internal static readonly Guid EMPRESA_DEMO_B_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("3b41ce3b-be6e-4e44-8c3e-87f6de12d818");
    internal static readonly Guid EMPRESA_DEMO_B_DEPARTAMENTO_COMERCIAL = Guid.Parse("104721f3-a3ec-4006-8906-f0204a2fedf2");
    internal static readonly Guid EMPRESA_DEMO_B_DIRECAO = Guid.Parse("e3b46221-33cd-4277-82a7-f1756cfe570c");

    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("Departamentos");

        builder.HasData(

        //EMPRESA DEMO A
        new Departamento
        {
            Id = EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO,
            Nome = "Departamento Administrativo",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },
        new Departamento
        {
            Id = EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL,
            Nome = "Departamento Comercial",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },
        new Departamento
        {
            Id = EMPRESA_DEMO_A_DIRECAO,
            Nome = "Direção",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },

        //EMPRESA DEMO B
        new Departamento
        {
            Id = EMPRESA_DEMO_B_DEPARTAMENTO_ADMINISTRATIVO,
            Nome = "Departamento Administrativo",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        },
        new Departamento
        {
            Id = EMPRESA_DEMO_B_DEPARTAMENTO_COMERCIAL,
            Nome = "Departamento Comercial",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        },
        new Departamento
        {
            Id = EMPRESA_DEMO_B_DIRECAO,
            Nome = "Direção",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        }
        );
    }
}