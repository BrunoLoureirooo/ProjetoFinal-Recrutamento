using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.TipoDocumento;

public class TipoDocumentoCreateCandidaturaDto
{
    public TipoDocumentoCreateCandidaturaDto()
    {
    }

    [Required(ErrorMessage = "ID do tipo de documento necessário")]
    public Guid TipoDocumentoId { get; set; }

    [Required(ErrorMessage = "Base64 do ficheiro a enviar necessária")]
    public string Ficheiro { get; set; }
}
