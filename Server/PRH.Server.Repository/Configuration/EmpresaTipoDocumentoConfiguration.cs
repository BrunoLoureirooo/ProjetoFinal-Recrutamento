using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using Bogus;

namespace PRH.Server.Repository.Configuration
{
    public class EmpresaTipoDocumentoConfiguration : IEntityTypeConfiguration<EmpresaTipoDocumento>
    {
        static EmpresaTipoDocumentoConfiguration()
        {
            Randomizer.Seed = new Random(9012);
        }

        public void Configure(EntityTypeBuilder<EmpresaTipoDocumento> builder)
        {
            builder.ToTable("EmpresaTipoDocumentos");

            builder.HasKey(etd => new { etd.EmpresaId, etd.TipoDocumentoId });

            builder.HasOne(etd => etd.Empresa)
                   .WithMany(e => e.EmpresaTipoDocumentos)
                   .HasForeignKey(etd => etd.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(etd => etd.TipoDocumento)
                   .WithMany(td => td.EmpresaTipoDocumentos)
                   .HasForeignKey(etd => etd.TipoDocumentoId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}