using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Entities.Models
{
    public class Funcionario : BaseModel
    {        
        public Guid EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public Guid EstabelecimentoId { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }

        public Guid DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        public Guid EquipaId { get; set; }
        public virtual Equipa Equipa { get; set; }

        public Guid FuncaoId { get; set; }
        public virtual Funcao Funcao { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Email { get; set; }
    }
}
