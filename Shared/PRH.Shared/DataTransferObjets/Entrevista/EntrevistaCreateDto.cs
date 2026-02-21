using PRH.Shared.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Entrevista;

public class EntrevistaCreateDto
{
    public EntrevistaCreateDto()
    {
    }

    [Required(ErrorMessage = "Entrevista tem de estar associada a uma candidatura")]
    public Guid CandidaturaId { get; set; }

    [Required(ErrorMessage = "Entrevista tem de estar marcada para um dia e hora")]
    [FutureDate(ErrorMessage = "Entrevista não pode estar marcada para o passado")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "Entrevista tem de ter um entrevistador")]
    public Guid EntrevistadorId { get; set; }
}
