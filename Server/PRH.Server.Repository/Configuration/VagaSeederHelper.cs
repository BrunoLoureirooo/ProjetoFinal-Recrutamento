using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Configuration;

namespace PRH.Server.Seeding
{
    public static class VagaSeederHelper
    {
        public static List<Vaga> GenerateVagas(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            // Buscar todos os recrutamentos do banco de dados (incluindo os gerados)
            var Recrutamentos = context.Recrutamentos.Select(r => r.Id).ToArray();

            var Tipo = context.TipoVagas.Select(r => r.Id).ToArray();

            var Modalidade = context.Modalidades.Select(r => r.Id).ToArray();

            var Cargo = context.Cargos.Select(c => c.Id).ToArray();

            var Bools = new[]
            {
                true,
                false
            };

            var vagaFaker = new Faker<Vaga>()
                .RuleFor(v => v.Id, f => Guid.NewGuid())
                .RuleFor(v => v.CandidaturaDisponivel, f => f.PickRandom(Bools)) // aleatório entre 0 e 1
                .RuleFor(v => v.RecrutamentoId, f => f.PickRandom(Recrutamentos)) // Pega aleatoriamente de todos os recrutamentos
                .RuleFor(v => v.TipoId, f => f.PickRandom(Tipo))
                .RuleFor(v => v.ModalidadeId, f => f.PickRandom(Modalidade))
                .RuleFor(v => v.NumVagas, f => f.Random.Int(1, 20))
                .RuleFor(v => v.Descricao, f => f.Lorem.Sentence(10))
                .RuleFor(v => v.CargoId, f => f.PickRandom(Cargo));

            return vagaFaker.Generate(count); // Gera a lista de vagas
        }
    }
}
