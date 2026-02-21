using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class OpcoesCampoLookupConfiguration : IEntityTypeConfiguration<OpcoesCampoLookup>
{

    public void Configure(EntityTypeBuilder<OpcoesCampoLookup> builder)
    {
        builder.ToTable("OpcoesCampoLookups");

        builder.HasOne(x => x.Campo)
            .WithMany(x => x.OpcoesCampoLookups)
            .HasForeignKey(x => x.CampoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.CampoId).
            IsRequired();
        builder.Property(x => x.Descricao).
            HasMaxLength(50).
            IsRequired();

    }
}