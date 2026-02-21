using System;

namespace PRH.Shared.DataTransferObjets
{
    public class EmpresaTipoDocumentoDto
    {
        public EmpresaTipoDocumentoDto()
        {
        }

        public Guid EmpresaId { get; set; }
        public Guid TipoDocumentoId { get; set; }
    }
}