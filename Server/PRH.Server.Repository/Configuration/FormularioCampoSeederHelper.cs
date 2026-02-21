using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Configuration;

namespace PRH.Server.Seeding
{
    public static class FormularioCampoSeederHelper
    {
        public static List<FormularioCampo> GenerateFormularioCampos(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var Formularios = context.Formularios.Select(r => r.Id).ToArray();

            var Campos = context.Campos.Select(c => c.Id).ToArray();

            var estados = new[]
            {
                true, false
            };

            var FormularioCampoFaker = new Faker<FormularioCampo>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.CampoId, f => f.PickRandom(Campos))
                .RuleFor(c => c.FormularioId, f => f.PickRandom(Formularios)) // Pode aleatorizar se quiser
                .RuleFor(c => c.Ativo, f => f.PickRandom(estados));
            return FormularioCampoFaker.Generate(count);
        }
    }
}
