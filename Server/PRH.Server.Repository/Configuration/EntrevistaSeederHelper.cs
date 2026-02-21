using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Configuration;

namespace PRH.Server.Seeding
{
    public static class EntrevistaSeederHelper
    {
        public static List<Entrevista> GenerateEntrevistas(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var candidaturas = context.Candidaturas
                .Select(c => new
                {
                    c.Id,
                    EstadoDescricao = c.Estado.Descricao
                })
                .ToList();

            var funcionarios = context.Funcionarios.Select(f => f.Id).ToArray();

            var resultados = context.Resultados
                .Select(r => new
                {
                    r.Id,
                    r.Descricao
                })
                .ToList();

            var resultadoAprovado = resultados.FirstOrDefault(r => r.Descricao == "Aprovado");
            var resultadoReprovado = resultados.FirstOrDefault(r => r.Descricao == "Reprovado");

            var entrevistas = new List<Entrevista>();

            // Passo 1: garantir pelo menos uma entrevista obrigatória por regra
            foreach (var candidatura in candidaturas)
            {
                Guid? resultadoIdObrigatorio = null;

                if (candidatura.EstadoDescricao == "Selecionado" && resultadoAprovado != null)
                    resultadoIdObrigatorio = resultadoAprovado.Id;

                if (candidatura.EstadoDescricao == "Rejeitado" && resultadoReprovado != null)
                    resultadoIdObrigatorio = resultadoReprovado.Id;

                if (resultadoIdObrigatorio != null)
                {
                    entrevistas.Add(new Entrevista
                    {
                        Id = Guid.NewGuid(),
                        CandidaturaId = candidatura.Id,
                        EntrevistadorId = faker.PickRandom(funcionarios),
                        DataHora = faker.Date.Past(3),
                        Notas = faker.Lorem.Sentence(100),
                        ResultadoEntrevistaId = resultadoIdObrigatorio.Value
                    });
                }
            }

            // Passo 2: preencher com entrevistas aleatórias, respeitando as regras
            while (entrevistas.Count < count)
            {
                var candidatura = faker.PickRandom(candidaturas);
                var entrevistadorId = faker.PickRandom(funcionarios);

                // Define possíveis resultados com base no estado da candidatura
                var resultadosPossiveis = resultados
                    .Where(r =>
                    {
                        if (candidatura.EstadoDescricao == "Selecionado")
                            return r.Descricao != "Entrevista Agendada" && r.Descricao != "Reprovado";
                        if (candidatura.EstadoDescricao == "Rejeitado")
                            return r.Descricao != "Entrevista Agendada" && r.Descricao != "Aprovado";
                        if (candidatura.EstadoDescricao == "Candidatura")
                            return r.Descricao != "Aprovado" && r.Descricao != "Reprovado";
                        return true;
                    })
                    .ToList();

                var resultadoEscolhido = resultadosPossiveis.Any()
                    ? faker.PickRandom(resultadosPossiveis)
                    : resultados.FirstOrDefault(r => r.Id == ResultadoConfiguration.Tipo_Resultado_Vazio);

                var dataHora = resultadoEscolhido.Descricao == "Entrevista Agendada"
                    ? faker.Date.Future(3)
                    : faker.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6));

                entrevistas.Add(new Entrevista
                {
                    Id = Guid.NewGuid(),
                    CandidaturaId = candidatura.Id,
                    EntrevistadorId = entrevistadorId,
                    DataHora = dataHora,
                    Notas = faker.Lorem.Sentence(100),
                    ResultadoEntrevistaId = resultadoEscolhido.Id
                });
            }

            return entrevistas;
        }
    }
}
