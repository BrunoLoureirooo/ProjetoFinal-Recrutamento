using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

namespace PRH.Server.Seeding
{
    public static class DepartamentoSeederHelper
    {
        private static readonly string[] NomesDepartamentos = new[]
        {
            "Departamento Administrativo",
            "Departamento Comercial",
            "Direção",
            "Departamento de Recursos Humanos",
            "Departamento de Tecnologia",
            "Departamento Jurídico"
        };

        public static List<Departamento> GenerateDepartamentos(int countPerEmpresa, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var empresaIds = context.Empresas.Select(e => e.Id).ToArray();

            var departamentos = new List<Departamento>();

            foreach (var empresaId in empresaIds)
            {
                var nomesUsados = new HashSet<string>();

                for (int i = 0; i < countPerEmpresa; i++)
                {
                    // Evita nomes repetidos por empresa
                    var nome = faker.PickRandom(NomesDepartamentos.Except(nomesUsados).ToArray());
                    nomesUsados.Add(nome);

                    departamentos.Add(new Departamento
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        EmpresaId = empresaId
                    });
                }
            }

            return departamentos;
        }
    }
}
