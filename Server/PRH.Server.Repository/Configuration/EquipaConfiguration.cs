using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;

public class EquipaConfiguration : IEntityTypeConfiguration<Equipa>
{
    internal static readonly Guid EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A = Guid.Parse("d6def626-d3cb-434c-bacd-c19f6b71719a");
    internal static readonly Guid EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL_EQUIPA_A = Guid.Parse("8b77b08c-b28d-4883-ba8f-776015311465");
    internal static readonly Guid EMPRESA_DEMO_A_DIRECAO_EQUIPA_A = Guid.Parse("44b2bdb4-d9ec-404b-90f9-46876d8c0e87");

    internal static readonly Guid EMPRESA_DEMO_B_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A =  Guid.Parse("5bd7206e-c5b0-4313-9156-bc76805f4773");
    internal static readonly Guid EMPRESA_DEMO_B_DEPARTAMENTO_COMERCIAL_EQUIPA_A = Guid.Parse("d2fe96ee-8128-43e4-8625-c0a1decd963c");
    internal static readonly Guid EMPRESA_DEMO_B_DIRECAO_EQUIPA_A = Guid.Parse("a31cb43a-41c4-45ef-b437-bd9951dd8dfd");


    public void Configure(EntityTypeBuilder<Equipa> builder)
    {
        builder.ToTable("Equipas"); 

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.Equipas)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Departamento)
            .WithMany(x => x.Equipas)
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(

        //EMPRESA DEMO A
        new Equipa
        {
            Id = EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            Nome = "Equipa A",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO,
        },
        new Equipa
        {
            Id = EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            Nome = "Equipa B",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL
        },
        new Equipa
        {
            Id = EMPRESA_DEMO_A_DIRECAO_EQUIPA_A,
            Nome = "Equipa C",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DIRECAO
        },

        //EMPRESA DEMO B
        new Equipa
        {
            Id = EMPRESA_DEMO_B_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            Nome = "Equipa A",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B, 
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_B_DEPARTAMENTO_ADMINISTRATIVO
        },
        new Equipa
        {
            Id = EMPRESA_DEMO_B_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            Nome = "Departamento Comercial",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_B_DEPARTAMENTO_COMERCIAL
        },
        new Equipa
        {
            Id = EMPRESA_DEMO_B_DIRECAO_EQUIPA_A,
            Nome = "Direção",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_B_DIRECAO
        }
        );
    }
}


