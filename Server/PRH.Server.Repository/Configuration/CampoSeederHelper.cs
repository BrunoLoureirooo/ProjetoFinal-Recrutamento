using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using System.Globalization;
using System.Text;


namespace PRH.Server.Seeding
{
    public static class CampoSeederHelper
    {
        public static List<Campo> GenerateCampos(int count, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");

            var bools = new[] { true, false };

            var tiposCampos = context.TipoCampos
                .Select(tc => new { tc.Id, tc.Descricao })
                .ToList();

            // Inicializa o tracker com os nomes do banco
            var nomeUnicoTracker = context.Campos
                .Select(c => c.NomeUnico)
                .GroupBy(n => n)
                .ToDictionary(g => g.Key, g => g.Count() - 1);

            var campoFaker = new Faker<Campo>()
                .CustomInstantiator(f =>
                {
                    var tipo = f.PickRandom(tiposCampos);
                    var Opcional = f.PickRandom(bools);

                    var nome = tipo.Descricao switch
                    {
                        "string" => f.PickRandom(new[] { "Nome completo", "Endereço", "Cidade", "Estado", "Nacionalidade", "Profissão", "Departamento", "Observações", "Descrição", "Responsável" }),
                        "anexo" => "Anexo " + f.PickRandom(new[] { "documentos pessoais", "comprovante de residência", "certificado de conclusão", "declaração de imposto de renda", "foto 3x4", "cópia do contrato", "currículo", "certidão negativa", "licença", "atestado médico" }),
                        "lookup" => "Selecione " + f.PickRandom(new[] { "departamento", "setor responsável", "supervisor", "cargo", "unidade de negócio", "empresa", "cliente", "fornecedor", "tipo de contrato", "região" }),
                        "booleano" => "É " + f.PickRandom(new[] { "ativo", "verificado", "concluído", "aprovado", "pago", "entregue", "cadastrado", "atualizado", "cancelado", "em dia" }) + " ?",
                        "número" => "Número de " + f.PickRandom(new[] { "dependentes", "dias de férias", "parcelas", "meses de experiência", "horas trabalhadas", "projetos finalizados", "documentos anexados", "tentativas", "número de acessos", "saldo de horas" }),
                        "data" => "Data de " + f.PickRandom(new[] { "nascimento", "admissão", "demissão", "criação", "última atualização", "envio", "vencimento", "conclusão", "pagamento", "agendamento" }),
                        _ => "Campo personalizado"
                    };

                    var nomeUnico = RemoverAcentos(nome.ToUpper()).Replace(" ", "_");

                    if (nomeUnicoTracker.ContainsKey(nomeUnico))
                    {
                        var count = nomeUnicoTracker[nomeUnico] + 1;
                        nomeUnicoTracker[nomeUnico] = count;
                        nomeUnico = $"{nomeUnico}_{count}";
                    }
                    else
                    {
                        nomeUnicoTracker[nomeUnico] = 0;
                    }

                    return new Campo
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        NomeUnico = nomeUnico,
                        IsOpcional = Opcional,
                        TipoId = tipo.Id
                    };
                });

            return campoFaker.Generate(count);
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
