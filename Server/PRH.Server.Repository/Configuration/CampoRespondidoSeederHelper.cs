using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

namespace PRH.Server.Seeding
{
    public static class CampoRespondidoSeederHelper
    {
        public static List<CampoRespondido> GenerateCamposRespondidos(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_PT");

            var formulariosRespondidos = context.FormularioRespondidos.Select(r => r.Id).ToArray();
            var campos = context.Campos.ToArray();  // Cada Campo já tem Nome e TipoCampo

            var camposRespondidos = new List<CampoRespondido>();

            for (int i = 0; i < count; i++)
            {
                var campo = faker.PickRandom(campos);

                var resposta = campo.TipoCampo?.Descricao switch
                {
                    "Texto" => faker.Lorem.Sentence(),
                    "Número" => faker.Random.Int(1, 100).ToString(),
                    "Booleano" => faker.Random.Bool().ToString(),
                    "Data" => faker.Date.Past().ToString("yyyy-MM-dd"),
                    _ => faker.Lorem.Word()
                };

                var campoRespondido = new CampoRespondido
                {
                    Id = Guid.NewGuid(),
                    Nome = campo.Nome,
                    Tipo = campo.TipoCampo?.Descricao ?? "Desconhecido",
                    Resposta = resposta,
                    FormularioRespondidoId = faker.PickRandom(formulariosRespondidos)
                };

                camposRespondidos.Add(campoRespondido);
            }

            return camposRespondidos;
        }


    }
}
