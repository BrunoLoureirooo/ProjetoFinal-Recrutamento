using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Documento : BaseModel
{
	// TemplateId and Template can be null
	public Guid? TemplateId { get; set; }
	public virtual Template? Template { get; set; }

	public required string Caminho { get; set; }
	public Guid CandidaturaId { get; set; }

	public Guid TipoDocumentoId { get; set; }
	public virtual TipoDocumento TipoDocumento { get; set; }

	public bool DocumentoAssinado { get; set; }

	public string? CamposPreenchidos { get; set; } 


}
