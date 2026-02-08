using System;
using System.Collections.ObjectModel;

namespace PRH.Server.Entities.Models;

public class Departamento : BaseModel
{
    public string Nome { get; set; }
    
    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }

    public Collection<Equipa> Equipas { get; set; }
    public Collection<Funcionario> Funcionarios { get; set; }
}
