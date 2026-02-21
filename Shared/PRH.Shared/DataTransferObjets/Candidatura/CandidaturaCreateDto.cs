using PRH.Shared.DataTransferObjets.TipoDocumento;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaCreateDto
{
    public CandidaturaCreateDto()
    {
    }

    [Required(ErrorMessage = "Candidatura precisa de NIF do candidato")]
    [Range(100000000, 999999999, ErrorMessage = "NIF do candidato não válido")]
    public int NIFcandidato { get; set; }

    [Required(ErrorMessage = "Candidatura precisa de nome do candidato")]
    [MaxLength(256, ErrorMessage = "Nome do candidato deve ter no máximo 256 caractéres")]
    public string NomeCandidato { get; set; }

    [Required(ErrorMessage = "Candidatura precisa de contacto do candidato")]
    [Range(200000000, 999999999, ErrorMessage = "Contacto do candidato não válido")]
    public int ContactoCandidato { get; set; }

    [Required(ErrorMessage = "Candidatura precisa de e-mail do candidato")]
    [EmailAddress(ErrorMessage = "E-mail do candidato não é válido")]
    [MaxLength(256, ErrorMessage = "E-mail do candidato deve ter no máximo 256 caractéres")]
    public string EmailCandidato { get; set; }

    [Required(ErrorMessage = "Candidatura precisa de estar associada a uma vaga")]
    public Guid? VagaId { get; set; }

    [Required(ErrorMessage = "Candidatura precisa de respostas")]
    [MinLength(1, ErrorMessage = "Candidatura precisa de pelo menos 1 resposta")]
    public Collection<CampoRespondidoCreateCandidaturaDto> CamposRespondidos { get; set; }
    public Collection<TipoDocumentoCreateCandidaturaDto> Documentos { get; set; } = [];
}
