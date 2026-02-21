using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Campo : BaseModel
{
	public string Nome { get; set; }
	public string NomeUnico { get; set; }
	public bool IsOpcional { get; set; }
	public virtual TipoCampo TipoCampo { get; set; }
	public Guid TipoId { get; set; }
	public virtual Collection<FormularioCampo> FormularioCampos { get; set; }
	public virtual Collection<OpcoesCampoLookup> OpcoesCampoLookups { get; set; }
	
}
