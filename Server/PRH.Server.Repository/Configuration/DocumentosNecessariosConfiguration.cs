using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;
public class DocumentosNecessariosConfiguration : IEntityTypeConfiguration<DocumentosNecessarios>
{
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_A = Guid.Parse("d7b4e9c1-3a5f-48f2-a6d0-8c12f7a59e3d");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_B = Guid.Parse("f2a9c7d4-5b3e-41d8-b0f6-8e1d3a72c9b5");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_C = Guid.Parse("f81cbd56-82f6-4122-9619-c985f5f55c41");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_D = Guid.Parse("76fd2228-5a8d-43bc-bcda-94b0a89693e5");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_E = Guid.Parse("ccd164bd-9c33-4691-91c8-4296f8bf96f9");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_F = Guid.Parse("5f10eb2e-26ef-4d4d-813c-8c0ed511f4d4");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_G = Guid.Parse("495307df-4b60-422a-bf5b-05b6f2519b4b");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_H = Guid.Parse("240062af-6a14-4f8a-a641-aa0a68f34b8d");
	internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_I = Guid.Parse("c7a36717-bdc5-4ef1-b837-237c21478420");
    internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_J = Guid.Parse("a02f5b5e-e6cb-4e17-8d5c-d4c4d6e3b0a1");
    internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_K = Guid.Parse("59d1ff72-3a46-4a85-88cd-6f7bcbe4fd19");
    internal static readonly Guid DOCUMENTO_NECESSARIO_DEMO_L = Guid.Parse("c4d6e2bb-6b8e-4fa0-9249-cd7b9e3fc89c");

    public void Configure(EntityTypeBuilder<DocumentosNecessarios> builder)
	{
		builder.ToTable("DocumentosNecessarios");

		builder.HasOne(x => x.Cargo)
			.WithMany(x => x.DocumentosNecessarios)
			.HasForeignKey(x => x.CargoId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(x => x.TipoDocumento)
			.WithMany(x => x.DocumentosNecessarios)
			.HasForeignKey(x => x.TipoDocumentoId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Property(x => x.CargoId).
			IsRequired();
		builder.Property(x => x.TipoDocumentoId).
			IsRequired();

		builder.HasData(
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_A,
			   CargoId = CargoConfiguration.Cargo_Porteiro,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_B,
			   CargoId = CargoConfiguration.Cargo_Engenheiro_Software,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1]
		   },
           new DocumentosNecessarios
           {
               Id = DOCUMENTO_NECESSARIO_DEMO_C,
               CargoId = CargoConfiguration.Cargo_Engenheiro_Software,
               TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2]
           },
           new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_D,
			   CargoId = CargoConfiguration.Cargo_Analista_Dados,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_E,
			   CargoId = CargoConfiguration.Cargo_Contador,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[3]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_F,
			   CargoId = CargoConfiguration.Cargo_Assistente_Administrativo,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[4]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_G,
			   CargoId = CargoConfiguration.Cargo_Especialista_RH,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[5]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_H,
			   CargoId = CargoConfiguration.Cargo_Recrutador,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[6]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_I,
			   CargoId = CargoConfiguration.Cargo_Atendente,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[7]
		   },
		   new DocumentosNecessarios
		   {
			   Id = DOCUMENTO_NECESSARIO_DEMO_J,
			   CargoId = CargoConfiguration.Cargo_Tecnico_Manutencao,
			   TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1]
		   },
           new DocumentosNecessarios
           {
               Id = DOCUMENTO_NECESSARIO_DEMO_K,
               CargoId = CargoConfiguration.Cargo_Comercial,
               TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[1]
           },
           new DocumentosNecessarios
           {
               Id = DOCUMENTO_NECESSARIO_DEMO_L,
               CargoId = CargoConfiguration.Cargo_Comercial,
               TipoDocumentoId = TipoDocumentoSeeder.AllTipoDocumentoIds[2]
           }
       );
	}
}