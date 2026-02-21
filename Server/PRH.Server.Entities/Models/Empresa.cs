using System.Collections.ObjectModel;

namespace PRH.Server.Entities.Models
{
    public class Empresa : BaseModel
    {
        public string Nome { get; set; }
        public virtual GestorCliente Gestor { get; set; }
        public virtual Collection<Equipa> Equipas { get; set; }
        public virtual Collection<Funcionario> Funcionarios { get; set; }
        public virtual Collection<Recrutamento> Recrutamentos { get; set; }
        public virtual Collection<Template> Templates { get; set; }
        public virtual ICollection<EmpresaTipoDocumento> EmpresaTipoDocumentos { get; set; }
        = new List<EmpresaTipoDocumento>();
    }
}