using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Entrevista : BaseModel
{
	public Guid CandidaturaId { get; set; }
	public virtual Candidatura Candidatura { get; set; }
	public Guid EntrevistadorId { get; set; }
	public virtual Funcionario Entrevistador { get; set; }
	public DateTime DataHora { get; set; }
	public string Notas { get; set; }
	public Guid ResultadoEntrevistaId { get; set; }
	public virtual Resultado Resultado { get; set; }

}
