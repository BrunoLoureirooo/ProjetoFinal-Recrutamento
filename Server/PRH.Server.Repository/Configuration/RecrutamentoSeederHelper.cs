using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Configuration;
using static System.Reflection.Metadata.BlobBuilder;

namespace PRH.Server.Seeding
{
    public static class RecrutamentoSeederHelper
    {
        public static List<Recrutamento> GenerateRecrutamentos(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var Empresas = context.Empresas.Select(r => r).ToArray();



            var RecrutamentoFaker = new Faker<Recrutamento>()
                .CustomInstantiator(f =>
                {
                    var empresa = f.PickRandom(Empresas);

                    var nome = "Recrutamento " + empresa.Nome + " " + f.Lorem.Sentence(1);

                    return new Recrutamento
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        Data = DateTime.Parse(f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6)).ToString("yyyy-MM-dd")),
                        EmpresaID = empresa.Id
                    };
                }); // Pode aleatorizar se quiser

            return RecrutamentoFaker.Generate(count);
        }
    }
}
