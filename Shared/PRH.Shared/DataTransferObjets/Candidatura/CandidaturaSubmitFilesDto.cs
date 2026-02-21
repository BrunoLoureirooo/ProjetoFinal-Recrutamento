using Microsoft.AspNetCore.Http;

namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaSubmitFilesDto
{
    public CandidaturaSubmitFilesDto()
    {
    }

    public List<IFormFile> Ficheiros { get; set; }
    public Guid TipoDocumentoId { get; set; }
}
