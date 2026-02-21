using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{

	internal static readonly Guid FUNCIONARIO_DEMO_A = Guid.Parse("ac42383e-9c00-4660-ace9-e71d942396a0");
    internal static readonly Guid FUNCIONARIO_DEMO_B = Guid.Parse("c646ceea-1e04-4e33-801b-7a8a29dc8390");
    internal static readonly Guid FUNCIONARIO_DEMO_C = Guid.Parse("6664832b-40fd-4ca9-80c5-ff50b2df3ec8");
	internal static readonly Guid FUNCIONARIO_DEMO_D = Guid.Parse("de568654-11aa-4dd0-82eb-c699ea722e5f");
    internal static readonly Guid FUNCIONARIO_DEMO_E = Guid.Parse("8050f14d-759c-4b2a-ae2a-bdfee3207ecd");
	internal static readonly Guid FUNCIONARIO_DEMO_F = Guid.Parse("c548fb8e-134e-4902-855a-22a20de02ade");
	internal static readonly Guid FUNCIONARIO_DEMO_G = Guid.Parse("9f40195f-5607-49c4-a478-6c942d6ef5a6");
    internal static readonly Guid FUNCIONARIO_DEMO_H = Guid.Parse("d4eb5d92-df45-4416-820a-8edeb679c383");
    internal static readonly Guid FUNCIONARIO_DEMO_I = Guid.Parse("68caf8ab-eecd-45b3-944f-6f4a75b5d226");
    internal static readonly Guid FUNCIONARIO_DEMO_J = Guid.Parse("8d1d8fab-5bb9-4d42-ae14-683692dcbddd");

	public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Departamento)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Estabelecimento)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.EstabelecimentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Equipa)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.EquipaId)
            .OnDelete(DeleteBehavior.NoAction);

            
        builder.HasOne(x => x.Funcao)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.FuncaoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(FuncionariosEmpresaDemoA());
    }

    public List<Funcionario> FuncionariosEmpresaDemoA()
    {
        return new List<Funcionario>() {
            new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_A,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DIRECAO,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DIRECAO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "apinto@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.ANA_PINTO.ToString()
        },
        new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_B,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "acasaco@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.ANA_CASACO.ToString()
        },        
        new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_C,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_B,
            Email = "cazevedo@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.CARLA_AZEVEDO.ToString()
        },
        new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_D,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "fdias@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.FILIPE_DIAS.ToString()
        },
                new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_E,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "frebelo@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.FILOMENA_REBELO.ToString()
        },
                new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_F,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "jfonseca@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.JOAO_FONSECA.ToString()
        },
                new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_G,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DIRECAO,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DIRECAO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "malves@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.MARIA_ALVES.ToString()
        },
                new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_H,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "sconceicao@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.SANDRA_CONCEICAO.ToString()
        },
                new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_I,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "lneves@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.LUIS_NEVES.ToString()
        },
                new Funcionario
        {        
            Id = FUNCIONARIO_DEMO_J,
            EmpresaId = AllEmpresaIds[0],
            EstabelecimentoId = EstabelecimentoConfiguration.Empresa_TRIGENIUS_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.Empresa_TRIGENIUS_FUNCAO_A,
            Email = "ocunha@Empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.OLGA_CUNHA.ToString()
        },               
        };
    }
}