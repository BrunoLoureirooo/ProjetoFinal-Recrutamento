using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class VagaConfiguration : IEntityTypeConfiguration<Vaga>
{

	internal static readonly Guid Vaga_DEMO_TRIGENIUS_SOFTWARE = Guid.Parse("f12a9c84-3d7e-45b6-b0f2-8e5d1c73a9d8");
	internal static readonly Guid Vaga_DEMO_TRIGENIUS_COMERCIAL = Guid.Parse("b7e4d12f-8c3a-49f6-a5d9-02c8b1e37f4a");



	public void Configure(EntityTypeBuilder<Vaga> builder)
	{
		builder.ToTable("Vagas");

		builder.HasOne(x => x.Tipo)
			.WithMany(x => x.Vagas)
			.HasForeignKey(x => x.TipoId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(x => x.Cargo)
			.WithMany(x => x.Vagas)
			.HasForeignKey(x => x.CargoId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(x => x.Recrutamento)
			.WithMany(x => x.Vagas)
			.HasForeignKey(x => x.RecrutamentoId)
			.OnDelete(DeleteBehavior.NoAction);


		builder.Property(x => x.NumVagas).
			HasMaxLength(5).
			IsRequired();
		builder.Property(x => x.Descricao).
			HasMaxLength(2000);
		builder.Property(x => x.CandidaturaDisponivel).
			HasMaxLength(1).
			IsRequired();
		builder.Property(x => x.TipoId).
			IsRequired();
		builder.Property(x => x.RecrutamentoId).
			IsRequired();
		builder.Property(x => x.CargoId).
			IsRequired();

		builder.HasData(
				new Vaga
				{
					Id = Vaga_DEMO_TRIGENIUS_SOFTWARE,
					CandidaturaDisponivel = true,
					RecrutamentoId = RecrutamentoConfiguration.Recrutamento_DEMO_TRIGENIUS_SOFTWARE,
					TipoId = TipoVagaConfiguration.Tipo_Vaga_FULL_TIME,
					ModalidadeId = ModalidadeConfiguration.Tipo_Modalidade_PRESENCIAL,
					NumVagas = 12,
					Descricao = "Estamos em busca de profissionais apaixonados por tecnologia para integrar nossa equipe de desenvolvimento de software na TRIGENIUS. Atuando em projetos desafiadores, você será responsável por projetar, desenvolver e implementar soluções inovadoras, trabalhando em colaboração com times multidisciplinares para atender às demandas de clientes em diversos setores.\r\n\r\nPrincipais responsabilidades:\r\n\r\n    Desenvolver e manter aplicações de alta performance e escalabilidade.\r\n\r\n    Participar ativamente das decisões de arquitetura e design de sistemas.\r\n\r\n    Garantir a qualidade do código, aplicando boas práticas de engenharia de software.\r\n\r\n    Colaborar com equipes de produto e design para entregar soluções que gerem impacto real.\r\n\r\nRequisitos:\r\n\r\n    Experiência prévia com desenvolvimento em C#, .NET ou tecnologias similares.\r\n\r\n    Capacidade de trabalhar de forma colaborativa em ambientes ágeis.\r\n\r\n    Desejável conhecimento em arquitetura de microsserviços e boas práticas de DevOps.",
					CargoId = CargoConfiguration.Cargo_Engenheiro_Software
				},
				new Vaga
				{
					Id = Vaga_DEMO_TRIGENIUS_COMERCIAL,
					CandidaturaDisponivel = true,
					RecrutamentoId = RecrutamentoConfiguration.Recrutamento_DEMO_TRIGENIUS_COMERCIAL,
					TipoId = TipoVagaConfiguration.Tipo_Vaga_PART_TIME,
					ModalidadeId = ModalidadeConfiguration.Tipo_Modalidade_HIBRIDO,
					NumVagas = 5,
					Descricao = "A TRIGENIUS está em busca de um(a) profissional dinâmico(a) e motivado(a) para integrar nossa equipe de vendas. O(a) Representante Comercial será responsável por prospectar novos clientes, apresentar soluções personalizadas e construir relacionamentos de longo prazo que impulsionem o crescimento da empresa.\r\n\r\nPrincipais responsabilidades:\r\n\r\n    Identificar oportunidades de negócio e prospectar novos clientes.\r\n\r\n    Realizar apresentações comerciais e negociações, buscando o fechamento de contratos.\r\n\r\n    Acompanhar o pós-venda para garantir a satisfação dos clientes.\r\n\r\n    Trabalhar em conjunto com o time de marketing para potencializar as estratégias comerciais.\r\n\r\nRequisitos:\r\n\r\n    Experiência prévia em vendas ou atendimento ao cliente.\r\n\r\n    Boa comunicação e habilidade de negociação.\r\n\r\n    Proatividade, organização e foco em resultados.",
					CargoId = CargoConfiguration.Cargo_Comercial,
				}
		);
    }
}