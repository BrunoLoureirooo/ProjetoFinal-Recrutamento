using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class FormularioRespondido : BaseModel
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public Guid CandidaturaId { get; set; }
    public virtual Candidatura Candidatura { get; set; }
    public virtual Collection<CampoRespondido> CampoRespondidos { get; set; }

}
