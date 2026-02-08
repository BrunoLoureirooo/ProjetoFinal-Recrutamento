using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;

public class EstabelecimentoConfiguration : IEntityTypeConfiguration<Estabelecimento>
{
    internal static readonly Guid EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE = Guid.Parse("9fc5fb53-6945-4587-98bc-ef4bb4926c5e");
    internal static readonly Guid EMPRESA_DEMO_B_ESTABELECIMENTO_SEDE = Guid.Parse("a9340a8f-31e2-474e-9372-d564996a33ab");

    public void Configure(EntityTypeBuilder<Estabelecimento> builder)
    {
        builder.ToTable("Estabelecimentos");

        builder.HasData(

        //DEMO A
        new Estabelecimento
        {
            Id = EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            Nome = "Sede",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A
        },

        //DEMO
        //  B
        new Estabelecimento
        {
            Id = EMPRESA_DEMO_B_ESTABELECIMENTO_SEDE,
            Nome = "Sede",
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_B
        }
        );
    }
}
