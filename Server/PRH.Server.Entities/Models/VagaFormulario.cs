using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class VagaFormulario : BaseModel
{
	public Guid FormularioId { get; set; }
	public Guid VagaId { get; set; }
	public virtual Formulario Formulario { get; set; }
	public virtual Vaga Vaga { get; set; }


}
