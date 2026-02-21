using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class TipoVaga : Lookup
{
	public virtual Collection<Vaga> Vagas { get; set; }
}
