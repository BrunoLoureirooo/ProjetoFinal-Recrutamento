using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Resultado : Lookup
{
	public virtual Collection<Entrevista> Entrevistas { get; set; }
}
