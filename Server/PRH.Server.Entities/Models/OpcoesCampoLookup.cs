using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class OpcoesCampoLookup : BaseModel
{
    public Guid Id { get; set; }
    public Guid CampoId { get; set; }
    public string Descricao { get; set; }

    public virtual Campo Campo { get; set; }
}
