using PRH.Shared.DataTransferObjets.CampoRespondido;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using PRH.Shared.DataTransferObjets;

namespace PRH.Shared.DataTransferObjets.Formulario;

public class FormularioCreateDto
{
    public FormularioCreateDto()
    {
    }
    [Required(ErrorMessage = "Formulario precisa de um nome")]
    [MaxLength(128, ErrorMessage = "Nome deve ter no máximo 128 caractéres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Formulario precisa de um tipo")]
    public LookupDto TipoFormulario { get; set; }

    [Required(ErrorMessage = "Formulario precisa de campos")]
    [MinLength(1, ErrorMessage = "Formulario precisa de pelo menos 1 campo")]
    public Collection<FormularioCampoDto> FormularioCampos { get; set; }

}
