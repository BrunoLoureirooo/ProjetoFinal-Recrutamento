using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("Templates");

            builder.Property(x => x.Caminho)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Preenchivel)
                   .IsRequired();

            builder.HasOne(x => x.TipoDocumento)
                   .WithMany(td => td.Templates)
                   .HasForeignKey(x => x.TipoDocumentoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Empresa)
                   .WithMany(e => e.Templates)
                   .HasForeignKey(x => x.EmpresaID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(TemplateSeeder.GetTemplates());
        }
    }
}