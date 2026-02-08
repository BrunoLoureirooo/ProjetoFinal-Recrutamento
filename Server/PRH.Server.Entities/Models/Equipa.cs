using System;
using System.Collections.ObjectModel;

namespace PRH.Server.Entities.Models;

public class Equipa : BaseModel
{
    public string Nome { get; set; }

    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
        
    public Guid DepartamentoId { get; set; }
    public Departamento Departamento { get; set; }
    public Collection<Funcionario> Funcionarios { get; set; }
}
