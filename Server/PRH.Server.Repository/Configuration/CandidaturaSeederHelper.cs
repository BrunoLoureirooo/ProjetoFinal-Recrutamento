using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Configuration;

namespace PRH.Server.Seeding
{
    public static class CandidaturaSeederHelper
    {
        public static List<Candidatura> GenerateCandidaturas(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_PT");

            var recrutamentos = context.Recrutamentos.Select(r => r.Id).ToArray();
            var vagas = context.Vagas.Select(r => r.Id).ToArray();
            var estados = context.Estados.Select(r => r.Id).ToArray();

            // Primeiros nomes típicos portugueses
            var primeirosNomes = new[]
            {
                "João", "Maria", "Tiago", "Inês", "Francisco", "Leonor", "Miguel", "Beatriz",
                "Diogo", "Ana", "Gonçalo", "Margarida", "André", "Carolina", "Pedro", "Matilde",
                "Rafael", "Sofia", "Bruno", "Catarina"
            };

            // Apelidos (sobrenomes) comuns em Portugal
            var ultimosNomes = new[]
            {
                "Silva", "Santos", "Ferreira", "Pereira", "Oliveira", "Costa", "Martins", "Rodrigues",
                "Sousa", "Fernandes", "Gomes", "Lopes", "Marques", "Carvalho", "Ramos", "Almeida"
            };

            var candidaturaFaker = new Faker<Candidatura>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.VagaId, f => f.PickRandom(vagas))
                .RuleFor(c => c.NIFcandidato, f => f.Random.Int(100000000, 399999999))
                .RuleFor(c => c.NomeCandidato, f => $"{f.PickRandom(primeirosNomes)} {f.PickRandom(ultimosNomes)}")
                .RuleFor(c => c.ContactoCandidato, f => f.Random.Int(910000000, 969999999))
                .RuleFor(c => c.EmailCandidato, (f, c) =>
                {
                    var nome = c.NomeCandidato.ToLower().Replace(" ", ".");
                    return $"{nome}@example.pt";
                })
                .RuleFor(c => c.EstadoId, f => f.PickRandom(estados));

            return candidaturaFaker.Generate(count);
        }

    }
}
