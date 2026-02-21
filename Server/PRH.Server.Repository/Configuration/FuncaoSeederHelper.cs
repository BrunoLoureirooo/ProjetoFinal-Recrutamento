using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;

namespace PRH.Server.Repository.Configuration
{
    public static class FuncaoSeederHelper
    {
        private static readonly string[] NomesFuncoes = new[]
        {
            "Técnico de Suporte",
            "Analista de Dados",
            "Gestor de Projeto",
            "Programador",
            "Consultor Comercial",
            "Designer UX/UI",
            "Administrador de Sistemas",
            "Especialista de Marketing",
            "Operador de Logística",
            "Coordenador de Recursos Humanos",
            "Auditor Interno",
            "Responsável de Qualidade",
            "Especialista Financeiro",
            "Engenheiro de Software",
            "Analista de Negócios"
        };

        public static List<Funcao> GenerateFuncoes(int quantidadePorEmpresa, RepositoryContext context)
        {
            var faker = new Faker("pt_BR");
            var funcoes = new List<Funcao>();

            var empresas = context.Empresas.ToList();

            foreach (var empresa in empresas)
            {
                var nomesDisponiveis = new List<string>(NomesFuncoes);

                for (int i = 0; i < quantidadePorEmpresa && nomesDisponiveis.Count > 0; i++)
                {
                    var nome = faker.PickRandom(nomesDisponiveis);
                    nomesDisponiveis.Remove(nome);

                    funcoes.Add(new Funcao
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        EmpresaId = empresa.Id
                    });
                }
            }

            return funcoes;
        }
    }
}
