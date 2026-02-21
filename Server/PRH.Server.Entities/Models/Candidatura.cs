using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Candidatura : BaseModel
{
	public Guid VagaId { get; set; }
	public int NIFcandidato { get; set; }
	public string NomeCandidato { get; set; }
	public int ContactoCandidato { get; set; }
	public string EmailCandidato { get; set; }
	public Guid EstadoId { get; set; }


	public virtual Vaga Vaga { get; set; }
	public virtual Estado Estado {  get; set; }
	public virtual Collection<FormularioRespondido> FormularioRespondidos { get; set; }
	public virtual Collection<Entrevista> Entrevistas { get; set; }

}
