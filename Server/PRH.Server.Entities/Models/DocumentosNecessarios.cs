using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class DocumentosNecessarios : BaseModel
{
	public Guid CargoId { get; set; }
	public virtual Cargo Cargo { get; set; }
	public Guid TipoDocumentoId { get; set; }
	public virtual TipoDocumento TipoDocumento { get; set; }



}
