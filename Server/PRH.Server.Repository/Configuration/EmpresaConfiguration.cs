using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using static PRH.Server.Repository.Configuration.EmpresaConfiguration;

namespace PRH.Server.Repository.Configuration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        internal static readonly Guid Empresa_TRIGENIUS = Guid.Parse("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79");
        internal static readonly Guid Empresa_TECH_INNOVATORS = Guid.Parse("8588bf94-6622-4b1d-8f9f-d1b97ed7dc29");
		internal static readonly Guid Empresa_MARKET_LEADERS = Guid.Parse("35827598-d25d-4d08-b504-134c2331221d");
		internal static readonly Guid Empresa_TECH_CORP = Guid.Parse("5c23d6bf-ecdb-4b98-99bb-20447f5bc13c");
		internal static readonly Guid Empresa_MARKET_X = Guid.Parse("1aaa8376-c563-4d60-8260-0f376660593e");
		internal static readonly Guid Empresa_FINANCE_PLUS = Guid.Parse("b03e1560-9c6a-4881-9224-362e1775da64");
		internal static readonly Guid Empresa_HEALTHCARE_SOLUTION = Guid.Parse("0758d87d-1a91-48e5-bfbb-ff7ab7b12171");
		internal static readonly Guid Empresa_EDU_INNOVATE = Guid.Parse("fb717a87-3f6b-4e5f-97d6-9676945f3c0c");
		internal static readonly Guid Empresa_CLEANING_CO = Guid.Parse("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32");

        public static readonly Guid[] AllEmpresaIds =
        [
			Empresa_TRIGENIUS,
            Empresa_TECH_INNOVATORS,
            Empresa_MARKET_LEADERS,
            Empresa_TECH_CORP,
            Empresa_MARKET_X,
            Empresa_FINANCE_PLUS,
            Empresa_HEALTHCARE_SOLUTION,
            Empresa_EDU_INNOVATE,
            Empresa_CLEANING_CO
        ];

		public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

			builder.HasData(
                new Empresa
                {
                    Id = Empresa_TRIGENIUS,
                    Nome = "TRIGÉNIUS",
                },
                new Empresa
				{
					Id = Empresa_TECH_INNOVATORS,
					Nome = "TECH INNOVATORS",
				},
				new Empresa
				{
					Id = Empresa_MARKET_LEADERS,
					Nome = "MARKET LEADERS",
				},
				new Empresa
				{
					Id = Empresa_TECH_CORP,
					Nome = "TECH CORP",
				},
				new Empresa
				{
					Id = Empresa_MARKET_X,
					Nome = "MARKET X",
				},
				new Empresa
				{
					Id = Empresa_FINANCE_PLUS,
					Nome = "FINANCE PLUS",
				},
				new Empresa
				{
					Id = Empresa_HEALTHCARE_SOLUTION,
					Nome = "HEALTHCARE SOLUTION",
				},
				new Empresa
				{
					Id = Empresa_EDU_INNOVATE,
					Nome = "EDU INNOVATE",
				},
				new Empresa
				{
					Id = Empresa_CLEANING_CO,
					Nome = "CLEANING CO",
				}
			);
		}
    }
}