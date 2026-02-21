using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class FormularioCampo : BaseModel
{
	public Guid FormularioId { get; set; }
	public virtual Formulario Formulario { get; set; }
	public Guid CampoId { get; set; }
	public virtual Campo Campo { get; set; }
	public bool Ativo {  get; set; }

}
