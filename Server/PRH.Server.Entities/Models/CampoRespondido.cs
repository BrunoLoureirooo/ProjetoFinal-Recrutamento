using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class CampoRespondido : BaseModel
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public string Resposta { get; set; }
    public Guid FormularioRespondidoId { get; set; }
    public virtual FormularioRespondido FormularioRespondido { get; set; }

}
