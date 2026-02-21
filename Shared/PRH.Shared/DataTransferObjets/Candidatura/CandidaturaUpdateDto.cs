using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaUpdateDto
{
    public CandidaturaUpdateDto()
    {
    }

    [Required(ErrorMessage = "Candidatura precisa de estado")]
    public Guid? EstadoId { get; set; }

}
