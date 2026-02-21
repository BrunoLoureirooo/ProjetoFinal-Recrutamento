using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class ResultadoConfiguration : IEntityTypeConfiguration<Resultado>
{
    internal static readonly Guid Tipo_Resultado_Vazio = Guid.Parse("6cf2183a-8658-4382-811e-620072e1e3f8");
    internal static readonly Guid Tipo_Resultado_Aprovado = Guid.Parse("6f6d7cc0-f4a7-4b7a-8fe4-890467b579f6");
	internal static readonly Guid Tipo_Resultado_Reprovado = Guid.Parse("387604b5-a3a8-4646-b7e8-eebe825e4b1e");
	internal static readonly Guid Tipo_Resultado_Proxima_Fase = Guid.Parse("3b6a96f0-4b0b-447c-b906-fd7539d00f99");

	public void Configure(EntityTypeBuilder<Resultado> builder)
	{
		builder.ToTable("Lookups");

		builder.HasData(


		//Resultado
		new Resultado
		{
			Id = Tipo_Resultado_Aprovado,
			Descricao = "Aprovado"
		},
		new Resultado
		{
			Id = Tipo_Resultado_Reprovado,
			Descricao = "Reprovado"
		},
		new Resultado
		{
			Id = Tipo_Resultado_Proxima_Fase,
			Descricao = "Encaminhado para proxima fase"
		},
        new Resultado
        {
            Id = Tipo_Resultado_Vazio,
            Descricao = "Entrevista Agendada"
        }
        );
	}
}