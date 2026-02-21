using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Formulario : BaseModel
{
	public string Nome { get; set; }
	public string NomeUnico { get; set; }
	public Guid TipoId { get; set; }
	public virtual TipoFormulario Tipo {  get; set; }
	public virtual Collection<FormularioCampo> FormularioCampos { get; set; }
	public virtual Collection<VagaFormulario> VagaFormularios { get; set; }

}
