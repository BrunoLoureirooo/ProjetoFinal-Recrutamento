using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration
{
       public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
       {

              public void Configure(EntityTypeBuilder<TipoDocumento> builder)
              {

                     builder.ToTable("TipoDocumentos");

                     builder.Property(x => x.Nome)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(x => x.Assinavel)
                            .IsRequired();

                     builder.HasMany(td => td.Documentos)
                            .WithOne(d => d.TipoDocumento)
                            .HasForeignKey(d => d.TipoDocumentoId)
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasMany(td => td.Templates)
                            .WithOne(t => t.TipoDocumento)
                            .HasForeignKey(t => t.TipoDocumentoId)
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasMany(td => td.EmpresaTipoDocumentos)
                            .WithOne(etd => etd.TipoDocumento)
                            .HasForeignKey(etd => etd.TipoDocumentoId)
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasMany(td => td.DocumentosNecessarios)
                            .WithOne(dn => dn.TipoDocumento)
                            .HasForeignKey(dn => dn.TipoDocumentoId)
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasData(TipoDocumentoSeeder.GetTipoDocumentos());
              }

       }
}