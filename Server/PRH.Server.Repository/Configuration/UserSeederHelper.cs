using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using System.Globalization;
using System.Text;

namespace PRH.Server.Repository.Configuration
{
    public static class UserSeederHelper
    {
        public static List<ApplicationUser> GenerateUsers(int count, RepositoryContext context)
        {

            // Primeiros nomes típicos portugueses
            var primeirosNomes = new[]
            {
                "João", "Maria", "Tiago", "Inês", "Francisco", "Leonor", "Miguel", "Beatriz",
                "Diogo", "Ana", "Gonçalo", "Margarida", "André", "Carolina", "Pedro", "Matilde",
                "Rafael", "Sofia", "Bruno", "Catarina"
            };

            // Apelidos (sobrenomes) comuns em Portugal
            var ultimosNomes = new[]
            {
                "Silva", "Santos", "Ferreira", "Pereira", "Oliveira", "Costa", "Martins", "Rodrigues",
                "Sousa", "Fernandes", "Gomes", "Lopes", "Marques", "Carvalho", "Ramos", "Almeida"
            };

            var bools = new[] { true, false };

            var userFaker = new Faker<ApplicationUser>()
                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.Nome, f => $"{f.PickRandom(primeirosNomes)} {f.PickRandom(ultimosNomes)}")
                .RuleFor(u => u.Email, (f, u) =>
                {
                    var nome = u.Nome.ToLower().Replace(" ", ".");
                    return $"{nome}@example.pt";
                })
                .RuleFor(u => u.NormalizedEmail, (f, u) =>
                {
                    var email = RemoverAcentos(u.Email.ToUpper()).Replace(" ", "_");
                    return email;
                })
                .RuleFor(u => u.EmailConfirmed, f => f.PickRandom(bools))
                .RuleFor(u => u.ConcurrencyStamp, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.SecurityStamp, f => Guid.NewGuid().ToString());

            return userFaker.Generate(count); // Gera a lista de vagas
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
