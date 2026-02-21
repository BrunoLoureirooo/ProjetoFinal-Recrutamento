using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
	internal static readonly Guid Tipo_Estado_Decorrer = Guid.Parse("4facf236-b3fc-4cab-bdef-5214f674dfec");
	internal static readonly Guid Tipo_Estado_Admitido = Guid.Parse("7f42b081-0924-487f-bef0-8edfb8b89766");
	internal static readonly Guid Tipo_Estado_Rejeitado = Guid.Parse("f812d64f-118a-4905-8242-f6790aacb1f0");

	public void Configure(EntityTypeBuilder<Estado> builder)
	{
		builder.ToTable("Lookups");

		builder.HasData(

			//Estado
			new Estado
			{
				Id = Tipo_Estado_Decorrer,

				Descricao = "A Decorrer"

			},
			new Estado
			{
				Id = Tipo_Estado_Admitido,

				Descricao = "Admitido"
			},
			new Estado
			{
				Id = Tipo_Estado_Rejeitado,

				Descricao = "Rejeitado"
			}
		);
	}
}