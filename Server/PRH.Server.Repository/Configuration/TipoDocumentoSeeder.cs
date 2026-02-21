using Bogus;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration
{
    public static class TipoDocumentoSeeder
    {
        private static readonly string[] TipoNomes = new[]
        {
            "Anexo",
            "Contrato de Trabalho",
            "Termo de Responsabilidade",
            "Contrato de Prestação de Serviços",
            "Manual de Utilização",
            "Contrato com Termo",
            "Comprovativo médico e de segurança no trabalho",
            "Política de Recursos Humanos",
            "Termo de Confidencialidade (NDA)",
            "Política de Privacidade",
            "Regulamento Interno",
        };

        private static readonly Lazy<TipoDocumento[]> _tipos =
            new Lazy<TipoDocumento[]>(() =>
            {
                var faker = new Faker<TipoDocumento>("pt_PT")
                    .UseSeed(5678)
                    .RuleFor(td => td.Id, f => f.Random.Guid())
                    .RuleFor(td => td.Assinavel, f => f.Random.Bool(0.4f));

                return TipoNomes
                    .Select(nome =>
                    {
                        var td = faker.Generate();
                        td.Nome = nome;
                        return td;
                    })
                    .ToArray();
            });

        public static TipoDocumento[] GetTipoDocumentos() => _tipos.Value;

        public static Guid[] AllTipoDocumentoIds => _tipos.Value.Select(t => t.Id).ToArray();
    }
}
