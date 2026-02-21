using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using System.Text.Json;

namespace PRH.Server.Repository.Configuration
{
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
       {
        internal static readonly Guid Documento_DEMO_A = Guid.Parse("C373CD33-E77E-473C-8C0B-73AEB85955EF");
        internal static readonly Guid Documento_DEMO_B = Guid.Parse("F5D01A84-7B92-4B2F-ACBD-C74999E326F2");
        internal static readonly Guid Documento_DEMO_C = Guid.Parse("E7A9B382-218F-4267-8F19-DB9B6F564D92");
        internal static readonly Guid Documento_DEMO_D = Guid.Parse("3F71C2D1-66C5-464D-90D5-9DA11D02DC2C");
        internal static readonly Guid Documento_DEMO_E = Guid.Parse("4be54f78-3793-4a0b-bf2b-145c37db5db4");
        internal static readonly Guid Documento_DEMO_F = Guid.Parse("f75de97b-5ac1-4f58-91f4-1e1b0bb5d421");
        internal static readonly Guid Documento_DEMO_G = Guid.Parse("dc3f1cf2-870c-4c09-bf9e-75cf18c6c8b7");
        internal static readonly Guid Documento_DEMO_H = Guid.Parse("dcb4a1d2-cd43-48c2-bbb2-2e6f0be62e63");
        internal static readonly Guid Documento_DEMO_I = Guid.Parse("1a8e2b19-2278-4565-b530-4e79a0d74a0f");
        internal static readonly Guid Documento_DEMO_J = Guid.Parse("0e9a32ec-01cf-4c88-9cf3-3db59a4df057");
        internal static readonly Guid Documento_DEMO_K = Guid.Parse("84e8d5a0-e7a0-4fa1-a3b4-896517ce3cb1");
        internal static readonly Guid Documento_DEMO_L = Guid.Parse("9d7a9c13-dff6-4a23-93b9-4f53d3a13a6c");

        public void Configure(EntityTypeBuilder<Documento> builder)
            {
                    builder.ToTable("Documentos");

                    builder.Property(x => x.Caminho)
                        .IsRequired()
                        .HasMaxLength(255);

                    builder.Property(x => x.DocumentoAssinado)
                        .IsRequired();

                    builder.Property(x => x.CamposPreenchidos)
                            .HasColumnType("nvarchar(max)")
                            .IsRequired(false);

                    builder.HasOne(x => x.TipoDocumento)
                        .WithMany(td => td.Documentos)
                        .HasForeignKey(x => x.TipoDocumentoId);

                    builder.HasOne(x => x.Template)
                        .WithMany(t => t.Documentos)
                        .HasForeignKey(x => x.TemplateId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired(false);
                builder.HasData(
                    new Documento
                    {
                        Id = Documento_DEMO_A,
                        Caminho = "/uploads/documentos/documento1.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_A,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_B,
                        Caminho = "/uploads/documentos/documento2.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_A,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_C,
                        Caminho = "/uploads/documentos/documento3.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_A,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[0],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_D,
                        Caminho = "/uploads/documentos/documento1.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_B,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_E,
                        Caminho = "/uploads/documentos/documento2.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_B,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_F,
                        Caminho = "/uploads/documentos/documento3.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_B,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[0],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_G,
                        Caminho = "/uploads/documentos/documento1.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_C,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_H,
                        Caminho = "/uploads/documentos/documento2.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_C,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_I,
                        Caminho = "/uploads/documentos/documento3.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_C,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[0],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_J,
                        Caminho = "/uploads/documentos/documento1.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_D,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_K,
                        Caminho = "/uploads/documentos/documento2.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_D,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2],
                        DocumentoAssinado = false
                    },
                    new Documento
                    {
                        Id = Documento_DEMO_L,
                        Caminho = "/uploads/documentos/documento3.pdf",
                        CandidaturaId = CandidaturaConfiguration.Candidatura_DEMO_D,
                        TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[0],
                        DocumentoAssinado = false
                    }
                );
            }
       }
}
