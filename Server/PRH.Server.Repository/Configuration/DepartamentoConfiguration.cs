using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration;
public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
	internal static readonly Guid Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("9fc5fb53-6945-4587-98bc-ef4bb4926c5e");
	internal static readonly Guid Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL = Guid.Parse("88754e7b-3fd7-4959-98d8-547aa2eb3da3");
	internal static readonly Guid Empresa_TRIGENIUS_DIRECAO = Guid.Parse("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41");

	internal static readonly Guid Empresa_TECH_INNOVATORS_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("a1a08f28-76f4-4fa0-a445-bf8615b9e5a4");
	internal static readonly Guid Empresa_TECH_INNOVATORS_DEPARTAMENTO_COMERCIAL = Guid.Parse("b3e0131b-34e7-4685-bd8b-d38da0a47467");
	internal static readonly Guid Empresa_TECH_INNOVATORS_DIRECAO = Guid.Parse("d11b81a2-e593-45b2-9b1e-d87052a6d51d");

	internal static readonly Guid Empresa_MARKET_LEADERS_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("a7b278ea-e441-4422-bd5e-505f10ab5180");
	internal static readonly Guid Empresa_MARKET_LEADERS_DEPARTAMENTO_COMERCIAL = Guid.Parse("cd290d3b-4292-4c79-95fb-767602b40843");
	internal static readonly Guid Empresa_MARKET_LEADERS_DIRECAO = Guid.Parse("e6d07c10-9285-4087-b698-7bfb5365635b");

	internal static readonly Guid Empresa_TECH_CORP_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("df428847-bff2-4f72-a226-9c8b6fe8cde6");
	internal static readonly Guid Empresa_TECH_CORP_DEPARTAMENTO_COMERCIAL = Guid.Parse("b6286791-274b-4731-99fe-2f5485a7a97e");
	internal static readonly Guid Empresa_TECH_CORP_DIRECAO = Guid.Parse("bfed76f8-bb0b-4029-b06b-b27c4c00f937");

	internal static readonly Guid Empresa_MARKET_X_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("c0c13e5d-6bb2-406f-a426-99b47d498b8d");
	internal static readonly Guid Empresa_MARKET_X_DEPARTAMENTO_COMERCIAL = Guid.Parse("0c7a22c9-5c78-4b7a-9e6b-12c38b7a94be");
	internal static readonly Guid Empresa_MARKET_X_DIRECAO = Guid.Parse("12cc5d4a-2b83-427b-b6ec-7ca25f3c8311");

	internal static readonly Guid Empresa_FINANCE_PLUS_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("2344f768-3897-4014-a703-b218a90b8883");
	internal static readonly Guid Empresa_FINANCE_PLUS_DEPARTAMENTO_COMERCIAL = Guid.Parse("70de2ea0-d67e-47b1-9094-b2392d8bbdf7");
	internal static readonly Guid Empresa_FINANCE_PLUS_DIRECAO = Guid.Parse("2c6b82b4-8a44-48e6-b54e-8ec317e0b8fe");

	internal static readonly Guid Empresa_HEALTHCARE_SOLUTION_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("49716a7d-60c5-4692-b1b5-815ef290e7b1");
	internal static readonly Guid Empresa_HEALTHCARE_SOLUTION_DEPARTAMENTO_COMERCIAL = Guid.Parse("3b05f51c-bdb4-428d-b9a7-0d5fa17e8f83");
	internal static readonly Guid Empresa_HEALTHCARE_SOLUTION_DIRECAO = Guid.Parse("779b7c9f-f02b-4b7f-bdfd-d7e290438b09");

	internal static readonly Guid Empresa_EDU_INNOVATE_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("5c68f7d0-d756-464f-b601-938d8fd957ba");
	internal static readonly Guid Empresa_EDU_INNOVATE_DEPARTAMENTO_COMERCIAL = Guid.Parse("ad6d7d65-9d91-4bb1-b201-7d4590790416");
	internal static readonly Guid Empresa_EDU_INNOVATE_DIRECAO = Guid.Parse("e5cf4ff5-0a32-41b5-a82f-22639cfd7c55");

	internal static readonly Guid Empresa_CLEANING_CO_DEPARTAMENTO_ADMINISTRATIVO = Guid.Parse("71a47f99-c88a-4a9b-b902-95ec7208b18a");
	internal static readonly Guid Empresa_CLEANING_CO_DEPARTAMENTO_COMERCIAL = Guid.Parse("91a99023-034e-4bfc-9471-ef3493b0a87c");
	internal static readonly Guid Empresa_CLEANING_CO_DIRECAO = Guid.Parse("98b3da93-caf0-4bfe-b0f3-6597d98e5458");

	public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("Departamentos");

		builder.HasData(
		//Empresa DEMO A
		new Departamento
		{
			Id = Empresa_TRIGENIUS_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[0]
		},
		new Departamento
		{
			Id = Empresa_TRIGENIUS_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[0]
		},
		new Departamento
		{
			Id = Empresa_TRIGENIUS_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[0]
		},
		// Empresa TECH INNOVATORS
		new Departamento
		{
			Id = Empresa_TECH_INNOVATORS_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[1]
		},
		new Departamento
		{
			Id = Empresa_TECH_INNOVATORS_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[1]
		},
		new Departamento
		{
			Id = Empresa_TECH_INNOVATORS_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[1]
		},

		// Empresa MARKET LEADERS
		new Departamento
		{
			Id = Empresa_MARKET_LEADERS_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[2]
		},
		new Departamento
		{
			Id = Empresa_MARKET_LEADERS_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[2]
		},
		new Departamento
		{
			Id = Empresa_MARKET_LEADERS_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[2]
		},

		// Empresa TECH CORP
		new Departamento
		{
			Id = Empresa_TECH_CORP_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[3]
		},
		new Departamento
		{
			Id = Empresa_TECH_CORP_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[3]
		},
		new Departamento
		{
			Id = Empresa_TECH_CORP_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[3]
		},

		// Empresa MARKET X
		new Departamento
		{
			Id = Empresa_MARKET_X_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[4]
		},
		new Departamento
		{
			Id = Empresa_MARKET_X_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[4]
		},
		new Departamento
		{
			Id = Empresa_MARKET_X_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[4]
		},

		// Empresa FINANCE PLUS
		new Departamento
		{
			Id = Empresa_FINANCE_PLUS_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[5]
		},
		new Departamento
		{
			Id = Empresa_FINANCE_PLUS_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[5]
		},
		new Departamento
		{
			Id = Empresa_FINANCE_PLUS_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[5]
		},

		// Empresa HEALTHCARE SOLUTION
		new Departamento
		{
			Id = Empresa_HEALTHCARE_SOLUTION_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[6]
		},
		new Departamento
		{
			Id = Empresa_HEALTHCARE_SOLUTION_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[6]
		},
		new Departamento
		{
			Id = Empresa_HEALTHCARE_SOLUTION_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[6]
		},

		// Empresa EDU INNOVATE
		new Departamento
		{
			Id = Empresa_EDU_INNOVATE_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[7]
		},
		new Departamento
		{
			Id = Empresa_EDU_INNOVATE_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[7]
		},
		new Departamento
		{
			Id = Empresa_EDU_INNOVATE_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[7]
		},

		// Empresa CLEANING CO
		new Departamento
		{
			Id = Empresa_CLEANING_CO_DEPARTAMENTO_ADMINISTRATIVO,
			Nome = "Departamento Administrativo",
			EmpresaId = AllEmpresaIds[8]
		},
		new Departamento
		{
			Id = Empresa_CLEANING_CO_DEPARTAMENTO_COMERCIAL,
			Nome = "Departamento Comercial",
			EmpresaId = AllEmpresaIds[8]
		},
		new Departamento
		{
			Id = Empresa_CLEANING_CO_DIRECAO,
			Nome = "Direção",
			EmpresaId = AllEmpresaIds[8]
		}
	);
	}
}