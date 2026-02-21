using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Recrutamento : BaseModel
{
	public Guid EmpresaID { get; set; }
	public DateTime Data {  get; set; }
	public string Nome { get; set; }
	public virtual Empresa Empresa { get; set; }

	public virtual Collection<Vaga> Vagas { get; set; }

}
