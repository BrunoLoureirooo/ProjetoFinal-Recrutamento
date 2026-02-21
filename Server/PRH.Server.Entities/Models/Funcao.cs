using System;
using System.Collections.ObjectModel;

namespace PRH.Server.Entities.Models;

public class Funcao : BaseModel
{
    public string Nome { get; set; }   

    public Guid EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }

    public virtual Collection<Funcionario> Funcionarios { get; set; }
}
