using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Shared.DataTransferObjets.Formulario
{
    public class FormularioUpdateDto
    {
        public FormularioUpdateDto()
        {
        }
        public string? Nome { get; set; }
        public string? NomeUnico { get; set; }
        public LookupDto? TipoFormulario { get; set; }
        public Collection<FormularioCampoDto>? FormularioCampos { get; set; }
    }
}
