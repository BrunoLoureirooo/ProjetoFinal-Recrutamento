using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Template : BaseModel
{
	public Guid TipoDocumentoId { get; set; }
	public virtual TipoDocumento TipoDocumento { get; set; }

	public required string Caminho { get; set; }

	public virtual Collection<Documento>? Documentos { get; set; }

	public required bool Preenchivel { get; set; } = false;

	public virtual Empresa Empresa { get; set; }

	public Guid EmpresaID { get; set; }
}
