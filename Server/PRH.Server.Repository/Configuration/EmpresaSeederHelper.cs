using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Configuration;

namespace PRH.Server.Seeding
{
    public static class EmpresaSeederHelper
    {
        public static List<Empresa> GenerateEmpresas(int count)
        {
            var faker = new Faker("pt_BR");

            var Nome = new[]
            {
                "TechNova Solutions", "InovaCorp", "Alpha Systems", "BlueWave Technologies",
                "Grupo Orion", "NexGen Digital", "Visionary Labs", "EcoSmart Ltda",
                "GlobalReach Inc.", "StartUpX", "DataMind Analytics", "PrimeSoft",
                "Infoworks", "CoreTech", "Skyline Ventures", "PixelCraft Studios",
                "MetaBridge", "BrightPath Consulting", "Quantum Solutions", "Sigma Group"
            };


            var ext = new[]
            {
                "Lda", "S.A.", "LTDA", "EIRELI", "ME", "EI", "SL", "GmbH",
                "Inc.", "Corp.", "LLC", "PLC", "NV", "BV", "SAS", "S.à r.l.",
                "AG", "Oy", "Pty Ltd", "Co."
            };


            var EmpresaFaker = new Faker<Empresa>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Nome, f => f.PickRandom(Nome) + " " + f.PickRandom(ext));

            return EmpresaFaker.Generate(count);
        }
    }
}
