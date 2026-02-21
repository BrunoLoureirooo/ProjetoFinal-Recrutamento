using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Estado : Lookup
{
	public virtual Collection<Candidatura> Candidaturas { get; set; }
}
