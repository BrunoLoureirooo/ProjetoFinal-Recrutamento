using Bogus;
using PRH.Server.Entities.Models;
using System.Text.Json;

namespace PRH.Server.Repository.Configuration
{
    public static class DocumentoSeeder
    {
        private static readonly string[] Caminhos = new[]
        {
            "/uploads/documentos/documento1.pdf",
            "/uploads/documentos/documento2.pdf",
            "/uploads/documentos/documento3.pdf",
            "/uploads/documentos/documento4.pdf",
            "/uploads/documentos/documento5.pdf"
        };


        public static List<Documento> GenerateDocumentos(int count, RepositoryContext context)
        {
            var candidaturaIds = context.Candidaturas.Select(c => c.Id).ToArray();
            if (candidaturaIds.Length == 0)
                return new List<Documento>();

            var templateIds = context.Templates.Select(t => t.Id).ToArray();
            var tipoDocumentoIds = context.TipoDocumentos.Select(t => t.Id).ToArray();

            if (tipoDocumentoIds.Length == 0)
                return new List<Documento>();

            var faker = new Faker<Documento>("pt_PT")
                .UseSeed(3456)
                .RuleFor(d => d.Id, f => f.Random.Guid())
                .RuleFor(d => d.TemplateId, f =>
                    f.Random.Bool(0.7f) 
                        ? f.PickRandom(templateIds)
                        : (Guid?)null
                )
                .RuleFor(d => d.Caminho, (f, d) =>
                    d.TemplateId.HasValue
                        ? $"/uploads/documentos/{Path.GetFileNameWithoutExtension(
                              TemplateSeeder.TemplateIdToCaminhoMap[d.TemplateId.Value]
                          )}.pdf"
                        : f.PickRandom(Caminhos)
                )
                .RuleFor(d => d.TipoDocumentoId, (f, d) =>
                    d.TemplateId.HasValue
                        ? TemplateSeeder.TemplateToTipoMap[d.TemplateId.Value]
                        : f.PickRandom(tipoDocumentoIds)
                )
                .RuleFor(d => d.CandidaturaId, f => f.PickRandom(candidaturaIds))
                .RuleFor(d => d.DocumentoAssinado, f => f.Random.Bool())
                .RuleFor(d => d.CamposPreenchidos, (f, d) =>
                    d.TemplateId.HasValue
                        ? null
                        : CryptoHelper.Encrypt(
                            JsonSerializer.Serialize(
                                Enumerable.Range(1, 5)
                                    .ToDictionary(
                                        i => $"campo{i}",
                                        i => (object)(i % 2 == 0
                                            ? f.Lorem.Word()
                                            : (object)f.Random.Number(0, 100))
                                    )
                            )
                        )
                );
            return faker.Generate(count);
        }
    }
}
