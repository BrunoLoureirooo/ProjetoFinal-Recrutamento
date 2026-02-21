using PRH.Shared.DataTransferObjets.CampoRespondido;
using System.Collections.ObjectModel;

namespace PRH.Shared.DataTransferObjets.Formulario;

public class FormularioRespondidoDto
{
    public FormularioRespondidoDto()
    {

    }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public Collection<CampoRespondidoDto> Respostas { get; set; }
}
