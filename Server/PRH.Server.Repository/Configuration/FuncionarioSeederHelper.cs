using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

public static class FuncionarioSeederHelper
{

    public static List<Funcionario> GenerateFuncionarios(int count, RepositoryContext context)
    {
        var faker = new Faker("pt_BR");

        var empresas = context.Empresas.Select(e => e.Id).ToList();
        var funcoes = context.Funcoes.Select(f => f.Id).ToList();

        var excludedGuids = new HashSet<Guid>
        {
            Guid.Parse("e3a5e7de-975c-4d0f-ad17-cd563e62789a"), // ANA_PINTO
            Guid.Parse("251e0a52-d589-4845-ada8-daeb08be76ea"), // ANA_CASACO
            Guid.Parse("024e414c-d9d9-477a-bcb5-c6777bc7d3f9"), // CARLA_AZEVEDO
            Guid.Parse("0eae7ece-6671-4f83-9caa-fc2d8eadd9ae"), // FILIPE_DIAS
            Guid.Parse("db21f4f6-0eb4-4905-a604-ae5ab05696a6"), // FILOMENA_REBELO
            Guid.Parse("f1cc0ba3-1050-43f7-86bd-98d4b7f7e216"), // JOAO_FONSECA
            Guid.Parse("804eef0f-2ec6-4b19-8b54-3373388f122b"), // MARIA_ALVES
            Guid.Parse("faf84edb-dd0b-4de9-90f9-3671335d01c7"), // SANDRA_CONCEICAO
            Guid.Parse("4b93b0ab-aed4-4eb1-a40b-900229359841"), // LUIS_NEVES
            Guid.Parse("620d12b3-1d00-4d7f-a262-795f004f83fe"), // OLGA_CUNHA
            Guid.Parse("fb874eba-9c48-4340-a690-bebb2195d379"), // ADMIN
            Guid.Parse("2e0f5a9b-9c94-4e3e-b8b0-8fd60b6ec7ce"), // GESTOR_TRIGENIUS
        };

        // Filtra usuários removendo os que estão na lista excluída
        var allUsers = context.Users.ToList();

        // Agora filtra no client-side:
        var users = allUsers
            .Where(u => !excludedGuids.Contains(Guid.Parse(u.Id)))
            .ToList();

        var funcionarios = new List<Funcionario>();

        var maxCount = Math.Min(count, users.Count);

        for (int i = 0; i < maxCount; i++)
        {
            var empresaId = faker.PickRandom(empresas);

            var departamentosDaEmpresa = context.Departamentos
                .Where(d => d.EmpresaId == empresaId)
                .ToList();

            if (!departamentosDaEmpresa.Any())
                continue;

            var departamento = faker.PickRandom(departamentosDaEmpresa);

            var equipasDoDepartamento = context.Equipas
                .Where(e => e.DepartamentoId == departamento.Id)
                .ToList();

            if (!equipasDoDepartamento.Any())
                continue;

            var equipa = faker.PickRandom(equipasDoDepartamento);

            var estabelecimentosDaEmpresa = context.Estabelecimentos
                .Where(est => est.EmpresaId == empresaId)
                .Select(est => est.Id)
                .ToList();

            if (!estabelecimentosDaEmpresa.Any())
                continue;

            var estabelecimentoId = faker.PickRandom(estabelecimentosDaEmpresa);

            var funcaoId = faker.PickRandom(funcoes);

            var user = faker.PickRandom(users);
            users.Remove(user);

            var funcionario = new Funcionario
            {
                Id = Guid.NewGuid(),
                EmpresaId = empresaId,
                DepartamentoId = departamento.Id,
                EquipaId = equipa.Id,
                EstabelecimentoId = estabelecimentoId,
                FuncaoId = funcaoId,
                ApplicationUserId = user.Id,
                Email = user.Email
            };

            funcionarios.Add(funcionario);
        }

        return funcionarios;
    }
}
