using System;
using System.Collections.ObjectModel;

namespace PRH.Server.Entities.Models
{
    public class TipoDocumento : BaseModel
    {
        public required string Nome { get; set; }
        public virtual Collection<DocumentosNecessarios> DocumentosNecessarios { get; set; } = new Collection<DocumentosNecessarios>();
        public required bool Assinavel { get; set; }
        public virtual Collection<Documento> Documentos { get; set; }
        public virtual Collection<Template> Templates { get; set; }
        public virtual ICollection<EmpresaTipoDocumento> EmpresaTipoDocumentos { get; set; }
    	= new List<EmpresaTipoDocumento>();
    }
}
