using Bogus;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration
{
    public static class TemplateSeeder
    {
        private static readonly string[] Caminhos = new[]
        {
            "/uploads/templates/template1.docx",
            "/uploads/templates/template2.docx",
            "/uploads/templates/template3.docx",
            "/uploads/templates/template4.docx",
            "/uploads/templates/template5.docx"
        };
        private static readonly Lazy<Template[]> _templates = new Lazy<Template[]>(() =>
        {
            var faker = new Faker<Template>("pt_PT")
                .UseSeed(9012)
                .RuleFor(t => t.Id, f => f.Random.Guid())
                .RuleFor(t => t.Caminho, f => f.PickRandom(Caminhos))
                .RuleFor(t => t.Preenchivel, f => f.Random.Bool(0.5f))
                .RuleFor(t => t.TipoDocumentoId, f => f.PickRandom(TipoDocumentoSeeder.AllTipoDocumentoIds))
                .RuleFor(t => t.EmpresaID, f => f.PickRandom(EmpresaConfiguration.AllEmpresaIds));

            return faker.Generate(15).ToArray();
        });

        public static Template[] GetTemplates() => _templates.Value;
        public static Guid[] AllTemplateIds => _templates.Value.Select(t => t.Id).ToArray();

        public static IReadOnlyDictionary<Guid, Guid> TemplateToTipoMap =>
            _templates.Value.ToDictionary(t => t.Id, t => t.TipoDocumentoId);

        public static IReadOnlyDictionary<Guid, string> TemplateIdToCaminhoMap =>
            _templates.Value.ToDictionary(t => t.Id, t => t.Caminho);
    }
}