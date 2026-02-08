using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace PRH.Server.Entities.Models;

public class Estabelecimento : BaseModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }

    public Collection<Funcionario> Funcionarios { get; set; }
}
