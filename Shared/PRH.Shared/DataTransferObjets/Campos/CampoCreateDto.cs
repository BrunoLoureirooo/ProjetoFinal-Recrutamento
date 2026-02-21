using PRH.Shared.DataTransferObjets.CampoRespondido;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Campo;

public class CampoCreateDto
{
    public CampoCreateDto()
    {
    }
    public Guid TipoId { get; set; }
    [Required(ErrorMessage = "Campo precisa de um nome")]
    [MaxLength(128, ErrorMessage = "Nome deve ter no máximo 128 caractéres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Campo precisa de especificar se e opcional")]
    public bool IsOpcional { get; set; }
    [Required(ErrorMessage = "Campo precisa de tipo")]
    public LookupDto TipoCampo { get; set; }
}
