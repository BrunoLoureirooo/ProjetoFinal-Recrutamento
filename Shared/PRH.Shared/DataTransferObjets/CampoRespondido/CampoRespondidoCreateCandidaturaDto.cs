using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets;

public class CampoRespondidoCreateCandidaturaDto
{
    public CampoRespondidoCreateCandidaturaDto()
    {
    }

    [Required(ErrorMessage = "Resposta precisa de estar associada a um campo")]
    public Guid? CampoId { get; set; }

    [Required(ErrorMessage = "Resposta precisa de uma resposta")]
    public string Resposta { get; set; }
}
