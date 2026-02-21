using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PRH.Server.Seeding
{
    public static class FormularioSeederHelper
    {
        public static List<Formulario> GenerateFormularios(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            // Mapear os tipos de formulário com suas descrições
            var tipos = context.TipoFormularios
                .Select(t => new { t.Id, t.Descricao })
                .ToList();

            // Nomes associados a cada tipo
            var nomesPorTipo = new Dictionary<string, string[]>
            {
                ["Habilitacoes"] = new[]
                {
                    "Formação Acadêmica",
                    "Curso em Andamento",
                    "Ano de Conclusão Previsto",
                    "Certificações e Cursos"
                },
                ["Dados Pessoais"] = new[]
                {
                    "Informações Pessoais",
                    "Contato Principal",
                    "Contato Alternativo",
                    "Endereço Completo"
                },
                ["Experiencia Profissional"] = new[]
                {
                    "Experiência Profissional",
                    "Projetos Pessoais",
                    "Idiomas",
                    "Carta de Apresentação"
                },
                ["Disponibilidade"] = new[]
                {
                    "Disponibilidade para Início",
                    "Expectativa Salarial",
                    "Áreas de Interesse"
                },
                ["Referencias"] = new[]
                {
                    "Referências Profissionais",
                    "Redes Sociais Profissionais",
                    "LinkedIn",
                    "Github"
                },
                ["Anexos"] = new[]
                {
                    "Portfólio Online",
                    "Informações Adicionais",
                    "Currículo Anexado",
                    "Documentos Extras"
                },
                ["Contratação"] = new[]
                {
                    "Termos de Aceitação",
                    "Data de Início Prevista",
                    "Documentos Obrigatórios",
                    "Condições Contratuais",
                    "Responsável pela Contratação"
                }

            };

            // Controlar nomes únicos
            var nomeUnicoTracker = new Dictionary<string, int>();

            var formularioFaker = new Faker<Formulario>()
                .CustomInstantiator(f =>
                {
                    var tipo = f.PickRandom(tipos);
                    var nomesParaTipo = nomesPorTipo[tipo.Descricao];
                    var nome = f.PickRandom(nomesParaTipo);
                    var nomeUnicoBase = RemoverAcentos(nome.ToUpper()).Replace(" ", "_");


                    // Garante unicidade
                    string nomeUnico;
                    if (nomeUnicoTracker.ContainsKey(nomeUnicoBase))
                    {
                        nomeUnicoTracker[nomeUnicoBase]++;
                        nomeUnico = $"{nomeUnicoBase}_{nomeUnicoTracker[nomeUnicoBase]}";
                    }
                    else
                    {
                        nomeUnicoTracker[nomeUnicoBase] = 0;
                        nomeUnico = nomeUnicoBase;
                    }

                    return new Formulario
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        NomeUnico = nomeUnico,
                        TipoId = tipo.Id
                    };
                });

            return formularioFaker.Generate(count);
        }

        public static string RemoverAcentos(string texto)
        {
            var normalizedString = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }

}
