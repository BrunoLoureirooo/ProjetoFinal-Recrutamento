using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Entities.Models
{
    public class Empresa : BaseModel
    {
        public string Nome { get; set; }

        public Collection<Equipa> Equipas { get; set; }
        public Collection<Funcionario> Funcionarios { get; set; }
    }
}
