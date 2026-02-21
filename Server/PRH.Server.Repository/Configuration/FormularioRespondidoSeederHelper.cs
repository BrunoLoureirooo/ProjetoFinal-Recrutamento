using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

namespace PRH.Server.Seeding
{
    public static class FormularioRespondidoSeederHelper
    {
        public static List<FormularioRespondido> GenerateFormulariosRespondidos(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_PT");

            // Obtemos os formulários reais com nome e tipo já associados
            var formularios = context.Formularios
                .Select(f => new { f.Nome, f.Tipo })
                .ToList();

            var candidaturas = context.Candidaturas.Select(r => r.Id).ToArray();

            var formularioFaker = new Faker<FormularioRespondido>()
                .CustomInstantiator(f =>
                {
                    var form = f.PickRandom(formularios);
                    var candidaturaId = f.PickRandom(candidaturas);

                    return new FormularioRespondido
                    {
                        Id = Guid.NewGuid(),
                        Nome = form.Nome,
                        Tipo = form.Tipo.Descricao,
                        CandidaturaId = candidaturaId
                    };
                });

            return formularioFaker.Generate(count);
        }
    }
}
