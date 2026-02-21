using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class TipoFormulario : Lookup
{
	public virtual Collection<Formulario> Formularios { get; set; }
}
