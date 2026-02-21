using System;
using System.Collections.Generic;

namespace PRH.Shared.DataTransferObjets
{
    public class EmpresaDto
    {
        public EmpresaDto()
        {
        }

        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
