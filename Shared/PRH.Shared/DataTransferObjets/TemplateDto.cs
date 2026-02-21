using System;

namespace PRH.Shared.DataTransferObjets
{
    public class TemplateDto
    {
        public TemplateDto()
        {
            TipoDocumentoNome = string.Empty;
            EmpresaNome = string.Empty;
            Caminho = string.Empty;
        }

        public Guid Id { get; set; }
        public Guid TipoDocumentoId { get; set; }
        public string TipoDocumentoNome { get; set; }
        public string EmpresaNome { get; set; }
        public string Caminho { get; set; }
        public bool Preenchivel { get; set; }
        public Guid EmpresaId { get; set; }
    }
}