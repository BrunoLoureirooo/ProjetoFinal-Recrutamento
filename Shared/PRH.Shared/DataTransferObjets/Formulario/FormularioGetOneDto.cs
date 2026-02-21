using System.Collections.ObjectModel;

namespace PRH.Shared.DataTransferObjets.Formulario;

public class FormularioGetOneDto
{
    public FormularioGetOneDto()
    {
    }
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string NomeUnico { get; set; }
    public LookupDto TipoFormulario { get; set; }
    public Collection<FormularioCampoDto> FormularioCampos { get; set; }
}
