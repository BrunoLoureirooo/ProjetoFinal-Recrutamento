using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Server.Entities.Models;

public class Lookup : BaseModel
{
	public string Descricao { get; set; }
    public string Tipo { get; set; }

}
