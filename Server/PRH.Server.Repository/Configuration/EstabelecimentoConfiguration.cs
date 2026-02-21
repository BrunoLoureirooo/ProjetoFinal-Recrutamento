using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration;

public class EstabelecimentoConfiguration : IEntityTypeConfiguration<Estabelecimento>
{
    internal static readonly Guid Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE = Guid.Parse("9fc5fb53-6945-4587-98bc-ef4bb4926c5e");

    public void Configure(EntityTypeBuilder<Estabelecimento> builder)
    {
        builder.ToTable("Estabelecimentos");

        builder.HasData(

        //DEMO A
        new Estabelecimento
        {
            Id = Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            Nome = "Sede",
            EmpresaId = AllEmpresaIds[0]
        }
        );
    }
}
