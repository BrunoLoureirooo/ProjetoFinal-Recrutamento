using Microsoft.AspNetCore.Http;

namespace PRH.Shared.DataTransferObjets;

public class FicheiroComTipoDocumentoDto
{
    public FicheiroComTipoDocumentoDto()
    {
    }

    public IFormFile F;
    public Guid T;
}
