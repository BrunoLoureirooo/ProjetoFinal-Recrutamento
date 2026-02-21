using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration;

public class EquipaConfiguration : IEntityTypeConfiguration<Equipa>
{
    internal static readonly Guid Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A = Guid.Parse("d6def626-d3cb-434c-bacd-c19f6b71719a");
    internal static readonly Guid Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL_EQUIPA_A = Guid.Parse("8b77b08c-b28d-4883-ba8f-776015311465");
    internal static readonly Guid Empresa_TRIGENIUS_DIRECAO_EQUIPA_A = Guid.Parse("44b2bdb4-d9ec-404b-90f9-46876d8c0e87");

    public void Configure(EntityTypeBuilder<Equipa> builder)
    {
        builder.ToTable("Equipas"); 

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.Equipas)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Departamento)
            .WithMany(x => x.Equipas)
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(

        //Empresa DEMO A
        new Equipa
        {
            Id = Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            Nome = "Equipa A",
            EmpresaId = AllEmpresaIds[0],
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO,
        },
        new Equipa
        {
            Id = Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            Nome = "Equipa A",
            EmpresaId = AllEmpresaIds[0],
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL
        },
        new Equipa
        {
            Id = Empresa_TRIGENIUS_DIRECAO_EQUIPA_A,
            Nome = "Equipa A",
            EmpresaId = AllEmpresaIds[0],
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DIRECAO
        }
        );
    }
}


