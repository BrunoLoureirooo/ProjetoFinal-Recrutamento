using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class FuncaoConfiguration : IEntityTypeConfiguration<Funcao>
{
    internal static readonly Guid EMPRESA_DEMO_A_FUNCAO_A = Guid.Parse("97c6a146-61de-4d8b-a343-b54d81b0432a");
    internal static readonly Guid EMPRESA_DEMO_A_FUNCAO_B = Guid.Parse("d75b5cb3-b1e4-4c46-9e7e-4ac763ac6d6e");
    internal static readonly Guid EMPRESA_DEMO_A_FUNCAO_C = Guid.Parse("37e401f4-8e10-481a-b86f-f1e832490022");


    internal static readonly Guid EMPRESA_DEMO_B_FUNCAO_A = Guid.Parse("74b62f70-b001-43e7-8202-e81e7ac48d67");
    internal static readonly Guid EMPRESA_DEMO_B_FUNCAO_B = Guid.Parse("f8013611-d94d-4684-ba5f-7b887e9e4674");
    internal static readonly Guid EMPRESA_DEMO_B_FUNCAO_C = Guid.Parse("449ac1d8-71df-42df-9598-eb171d2c436e");


    public void Configure(EntityTypeBuilder<Funcao> builder)
    {
        builder.ToTable("Funcoes");

        builder.HasData(

        //DEMO A
        new Funcao
        {
            Id = EMPRESA_DEMO_A_FUNCAO_A,
            Nome = "Comercial",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },
        new Funcao
        {
            Id = EMPRESA_DEMO_A_FUNCAO_B,
            Nome = "Programador",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },
        new Funcao
        {
            Id = EMPRESA_DEMO_A_FUNCAO_C,
            Nome = "Técnico",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },

        //DEMO
        //  B
        new Funcao
        {
            Id = EMPRESA_DEMO_B_FUNCAO_A,
            Nome = "Comercial",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        },
        new Funcao
        {
            Id = EMPRESA_DEMO_B_FUNCAO_B,
            Nome = "Programador",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        },
        new Funcao
        {
            Id = EMPRESA_DEMO_B_FUNCAO_C,
            Nome = "Técnico",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        }
        );
    }
}
