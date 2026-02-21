using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

namespace PRH.Server.Seeding
{
    public static class EstabelecimentoSeederHelper
    {
        private static readonly string[] NomesEstabelecimentos = new[]
        {
            "Sede",
            "Filial Norte",
            "Filial Sul",
            "Unidade Centro",
            "Unidade Leste",
            "Posto Avançado",
            "Base Operacional",
            "Escritório Regional",
            "Ponto de Apoio",
            "Central Administrativa"
        };

        public static List<Estabelecimento> GenerateEstabelecimentos(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var empresaIds = context.Empresas.Select(e => e.Id).ToArray();

            var estabelecimentoFaker = new Faker<Estabelecimento>()
                .RuleFor(e => e.Id, f => Guid.NewGuid())
                .RuleFor(e => e.Nome, f => f.PickRandom(NomesEstabelecimentos))
                .RuleFor(e => e.EmpresaId, f => f.PickRandom(empresaIds));

            return estabelecimentoFaker.Generate(count);
        }
    }
}
