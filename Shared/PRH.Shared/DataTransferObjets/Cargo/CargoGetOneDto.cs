using PRH.Shared.DataTransferObjets.TipoDocumento;
using System.Collections.ObjectModel;

namespace PRH.Shared.DataTransferObjets.Cargo;

public class CargoGetOneDto
{
    public CargoGetOneDto()
    {
    }
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public Collection<TipoDocumentoGetOneCargoDto> TiposDocumento { get; set; }
}
