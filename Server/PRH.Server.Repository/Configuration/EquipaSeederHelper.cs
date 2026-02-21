using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

namespace PRH.Server.Seeding
{
    public static class EquipaSeederHelper
    {
        private static readonly string[] NomesEquipas = new[]
        {
            "Equipa A",
            "Equipa B",
            "Equipa C",
            "Equipa D",
            "Equipa de Projeto",
            "Equipa Técnica",
            "Equipa de Suporte"
        };


        public static List<Equipa> GenerateEquipas(int countPerDepartamento, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var departamentos = context.Departamentos.ToList();
            var equipas = new List<Equipa>();

            foreach (var departamento in departamentos)
            {
                var nomesUsados = new HashSet<string>();

                for (int i = 0; i < countPerDepartamento; i++)
                {
                    var nome = faker.PickRandom(NomesEquipas.Except(nomesUsados).ToArray());
                    nomesUsados.Add(nome);

                    equipas.Add(new Equipa
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        EmpresaId = departamento.EmpresaId,
                        DepartamentoId = departamento.Id
                    });
                }
            }

            return equipas;
        }
    }
}
