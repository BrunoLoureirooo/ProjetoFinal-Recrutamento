using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace PRH.Server.Seeding
{
    public static class OpcoesCampoLookupSeederHelper
    {
        public static List<OpcoesCampoLookup> GenerateOpcoes(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var nomesLookup = new[]
            {
                "Estado civil",
                "Gênero",
                "Tipo sanguíneo",
                "Nível de escolaridade",
                "Tipo de contrato",
                "Departamento",
                "Setor",
                "Cargo",
                "Região",
                "Unidade",
                "Tipo de documento",
                "Motivo da ausência",
                "Status do processo",
                "Status do pagamento",
                "Categoria do item",
                "Tipo de produto",
                "Nacionalidade",
                "País de origem",
                "Estado (UF)",
                "Município",
                "Idioma",
                "Turno de trabalho",
                "Área de atuação",
                "Tipo de atendimento",
                "Tipo de cliente",
                "Fonte de recrutamento",
                "Tipo de licença",
                "Modalidade de trabalho",
                "Tipo de projeto",
                "Prioridade",
                "Tipo de veículo",
                "Segmento de mercado",
                "Classificação de risco",
                "Tipo de benefício",
                "Tipo de despesa",
                "Forma de pagamento",
                "Banco",
                "Agência",
                "Nível hierárquico",
                "Grupo econômico",
                "Classificação tributária"
            };




            var Campos = (from campo in context.Campos
                          join tipo in context.Lookups on campo.TipoId equals tipo.Id
                          where tipo.Tipo == "Campo" && tipo.Descricao == "Lookup"
                          select campo.Id).ToArray();

            // Dictionary to track occurrences of NomeUnico to ensure uniqueness
            var nomeUnicoTracker = new Dictionary<string, int>();

            var campoFaker = new Faker<OpcoesCampoLookup>()
                .CustomInstantiator(f =>
                {
                    var CampoId = f.PickRandom(Campos);
                    var descricao = f.PickRandom(nomesLookup);

                    return new OpcoesCampoLookup
                    {
                        Id = Guid.NewGuid(),
                        CampoId = CampoId,
                        Descricao = descricao
                    };
                });

            return campoFaker.Generate(count);
        }

    }
}
