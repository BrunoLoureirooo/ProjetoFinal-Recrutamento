using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Departamento)
            .WithMany(x => x.Funcionarios)
            .HasForeignKey(x => x.EstabelecimentoId)
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
            Id = Guid.Parse("ac42383e-9c00-4660-ace9-e71d942396a0"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DIRECAO,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DIRECAO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "apinto@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.ANA_PINTO.ToString()
        },
        new Funcionario
        {        
            Id = Guid.Parse("c646ceea-1e04-4e33-801b-7a8a29dc8390"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "acasaco@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.ANA_CASACO.ToString()
        },        
        new Funcionario
        {        
            Id = Guid.Parse("6664832b-40fd-4ca9-80c5-ff50b2df3ec8"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_B,
            Email = "cazevedo@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.CARLA_AZEVEDO.ToString()
        },
        new Funcionario
        {        
            Id = Guid.Parse("de568654-11aa-4dd0-82eb-c699ea722e5f"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "fdias@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.FILIPE_DIAS.ToString()
        },
                new Funcionario
        {        
            Id = Guid.Parse("8050f14d-759c-4b2a-ae2a-bdfee3207ecd"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "frebelo@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.FILOMENA_REBELO.ToString()
        },
                new Funcionario
        {        
            Id = Guid.Parse("c548fb8e-134e-4902-855a-22a20de02ade"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "jfonseca@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.JOAO_FONSECA.ToString()
        },
                new Funcionario
        {        
            Id = Guid.Parse("9f40195f-5607-49c4-a478-6c942d6ef5a6"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DIRECAO,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DIRECAO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "malves@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.MARIA_ALVES.ToString()
        },
                new Funcionario
        {        
            Id = Guid.Parse("d4eb5d92-df45-4416-820a-8edeb679c383"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "sconceicao@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.SANDRA_CONCEICAO.ToString()
        },
                new Funcionario
        {        
            Id = Guid.Parse("68caf8ab-eecd-45b3-944f-6f4a75b5d226"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_COMERCIAL_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "lneves@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.LUIS_NEVES.ToString()
        },
                new Funcionario
        {        
            Id = Guid.Parse("8d1d8fab-5bb9-4d42-ae14-683692dcbddd"),
            EmpresaId = EmpresaConfiguration.EMPRESA_DEMO_A,
            EstabelecimentoId = EstabelecimentoConfiguration.EMPRESA_DEMO_A_ESTABELECIMENTO_SEDE,
            DepartamentoId = DepartamentoConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO,
            EquipaId = EquipaConfiguration.EMPRESA_DEMO_A_DEPARTAMENTO_ADMINISTRATIVO_EQUIPA_A,
            FuncaoId = FuncaoConfiguration.EMPRESA_DEMO_A_FUNCAO_A,
            Email = "ocunha@empresa_demo_a.pt",
            ApplicationUserId = UserConfiguration.OLGA_CUNHA.ToString()
        },               
        };
    }
}