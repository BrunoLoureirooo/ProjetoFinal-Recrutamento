using Bogus;
using PRH.Server.Entities.Models;


namespace PRH.Server.Repository.Configuration
{
    public static class EmpresaTipoDocumentoSeeder
    {

        private static readonly Lazy<EmpresaTipoDocumento[]> _entries = new Lazy<EmpresaTipoDocumento[]>(() =>
        {
            // mesma seed para reproducibilidade
            Randomizer.Seed = new Random(9012);
            var faker = new Faker("pt_PT");
            var list = new List<EmpresaTipoDocumento>();

            foreach (var empresaId in EmpresaConfiguration.AllEmpresaIds)
            {
                // entre 1 e 3 documentos por empresa
                int count = faker.Random.Int(1, 3);
                var tipoIds = faker
                    .PickRandom(TipoDocumentoSeeder.AllTipoDocumentoIds, count)
                    .Distinct();

                foreach (var tipoId in tipoIds)
                {
                    list.Add(new EmpresaTipoDocumento
                    {
                        EmpresaId = empresaId,
                        TipoDocumentoId = tipoId
                    });
                }
            }

            return list.ToArray();
        });

        // retorna todos os registros a semear
        public static EmpresaTipoDocumento[] GetEmpresaTipoDocumentos()
            => _entries.Value;

        // mapeia cada empresa aos seus tipos de documento
        public static IReadOnlyDictionary<Guid, Guid[]> EmpresaToTipoMap
            => _entries.Value
                       .GroupBy(e => e.EmpresaId)
                       .ToDictionary(
                           g => g.Key,
                           g => g.Select(e => e.TipoDocumentoId).ToArray()
                        );
    }
}