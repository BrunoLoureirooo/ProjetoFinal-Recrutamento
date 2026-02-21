using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration;
public class FuncaoConfiguration : IEntityTypeConfiguration<Funcao>
{
    internal static readonly Guid Empresa_TRIGENIUS_FUNCAO_A = Guid.Parse("97c6a146-61de-4d8b-a343-b54d81b0432a");
    internal static readonly Guid Empresa_TRIGENIUS_FUNCAO_B = Guid.Parse("d75b5cb3-b1e4-4c46-9e7e-4ac763ac6d6e");
    internal static readonly Guid Empresa_TRIGENIUS_FUNCAO_C = Guid.Parse("37e401f4-8e10-481a-b86f-f1e832490022");


    public void Configure(EntityTypeBuilder<Funcao> builder)
    {
        builder.ToTable("Funcoes");

        builder.HasData(

        //DEMO A
        new Funcao
        {
            Id = Empresa_TRIGENIUS_FUNCAO_A,
            Nome = "Comercial",
            EmpresaId = AllEmpresaIds[0]
        },
        new Funcao
        {
            Id = Empresa_TRIGENIUS_FUNCAO_B,
            Nome = "Programador",
            EmpresaId = AllEmpresaIds[0]
        },
        new Funcao
        {
            Id = Empresa_TRIGENIUS_FUNCAO_C,
            Nome = "Técnico",
            EmpresaId = AllEmpresaIds[0]
        }
        );
    }
}