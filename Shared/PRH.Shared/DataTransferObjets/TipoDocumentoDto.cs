using System.Collections.Generic;

namespace PRH.Shared.DataTransferObjets
{
    public class TipoDocumentoDto
    {
        public TipoDocumentoDto()
        {
            Nome = string.Empty;
            Documentos = new List<DocumentoDto>();
            Templates = new List<TemplateDto>();
            EmpresaTipoDocumentos = new List<EmpresaTipoDocumentoDto>();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Assinavel { get; set; }
        public ICollection<DocumentoDto> Documentos { get; set; }
        public ICollection<TemplateDto> Templates { get; set; }
        public ICollection<EmpresaTipoDocumentoDto> EmpresaTipoDocumentos { get; set; }
    }
}
