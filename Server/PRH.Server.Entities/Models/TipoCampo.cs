using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class TipoCampo : Lookup
{
	public virtual Collection<Campo> Campos { get; set; }
}
