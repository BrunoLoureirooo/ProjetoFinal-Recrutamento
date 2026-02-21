using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Configuration;

namespace PRH.Server.Seeding
{
    public static class CargoSeederHelper
    {
        public static List<Cargo> GenerateCargos(int count)
        {
            var faker = new Faker("pt_BR");

            var Titulo = new[]
             {
                "Presidente", "Vice-Presidente", "Diretor Executivo", "Diretor Financeiro",
                "Gerente de Projetos", "Coordenador", "Supervisor", "Chefe de Departamento",
                "Responsável Técnico", "Analista Sênior", "Analista Júnior", "Consultor",
                "Especialista", "Estagiário", "Assistente Administrativo", "Secretário",
                "Assessor", "Adjunto", "Representante", "Líder de Equipe"
            };

            var posicao = new[]
            {
                "Marketing", "Recursos Humanos", "Financeiro", "Contabilidade",
                "Tecnologia da Informação", "Desenvolvimento de Software", "Suporte Técnico",
                "Jurídico", "Compras", "Logística", "Vendas", "Comercial",
                "Pesquisa e Desenvolvimento", "Engenharia", "Atendimento ao Cliente",
                "Planejamento Estratégico", "Design Gráfico", "Comunicação Corporativa",
                "Gestão de Projetos", "Operações"
            };




            var campoFaker = new Faker<Cargo>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Nome, f => f.PickRandom(Titulo) + " " + f.PickRandom(posicao));

            return campoFaker.Generate(count);
        }
    }
}
