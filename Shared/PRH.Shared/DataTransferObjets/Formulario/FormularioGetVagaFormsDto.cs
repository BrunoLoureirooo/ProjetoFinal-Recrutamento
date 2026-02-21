using System.Collections.ObjectModel;
using PRH.Shared.DataTransferObjets.Campos;

namespace PRH.Shared.DataTransferObjets.Formulario;

public class FormularioGetVagaFormsDto
{
    public FormularioGetVagaFormsDto()
    {
    }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public Collection<CampoGetVagaFormsDto> Campos { get; set; }
}
