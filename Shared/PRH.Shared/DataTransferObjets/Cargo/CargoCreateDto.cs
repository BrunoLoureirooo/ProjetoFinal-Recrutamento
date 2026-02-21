using PRH.Shared.DataTransferObjets.TipoDocumento;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Cargo;

public class CargoCreateDto
{
    public CargoCreateDto()
    {
    }
    [Required(ErrorMessage = "Cargos precisa de um nome")]
    [MaxLength(100, ErrorMessage = "Nome do cargo deve ter no máximo 100 caractéres")]
    public string Nome { get; set; }
    public Collection<Guid> DocumentosNecessarios { get; set; } = [];
}
