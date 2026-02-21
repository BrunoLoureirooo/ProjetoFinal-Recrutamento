namespace PRH.Shared.DataTransferObjets.TipoDocumento;

public class TipoDocumentoGetOneCargoDto
{
    public TipoDocumentoGetOneCargoDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public bool Assinavel { get; set; }
}
