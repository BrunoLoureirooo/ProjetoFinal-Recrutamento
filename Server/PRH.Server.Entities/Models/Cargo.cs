using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Cargo : BaseModel
{
	public string Nome { get; set; }
	public virtual Collection<Vaga> Vagas { get; set; }
	public virtual Collection<DocumentosNecessarios> DocumentosNecessarios { get; set; }


}
