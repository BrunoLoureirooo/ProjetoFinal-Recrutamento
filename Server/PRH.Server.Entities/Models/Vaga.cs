using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Vaga : BaseModel
{
	public Guid RecrutamentoId { get; set; }
	public bool CandidaturaDisponivel { get; set; }
	public Guid TipoId { get; set; }
	public Guid ModalidadeId { get; set; }
	public int NumVagas { get; set; }
	public string Descricao { get; set; }
	public Guid CargoId { get; set; }

	public virtual Recrutamento Recrutamento { get; set; }
	public virtual Cargo Cargo { get; set; }
	public virtual TipoVaga Tipo { get; set; }
	public virtual Modalidade Modalidade { get; set; }
	public virtual Collection<Candidatura> Candidaturas {  get; set; }	
	public virtual Collection<VagaFormulario> VagasFormularios { get; set; }


}
