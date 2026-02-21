using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.CampoRespondido;

public class CampoRespondidoDto
{
    public CampoRespondidoDto()
    {
    }

    public Guid Id { get; set; }
    [Required(ErrorMessage = "Field name is a required field.")]
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public string Resposta { get; set; }
}
