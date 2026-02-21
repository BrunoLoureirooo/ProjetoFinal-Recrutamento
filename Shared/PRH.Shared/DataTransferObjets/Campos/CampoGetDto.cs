namespace PRH.Shared.DataTransferObjets.Campo;

public class CampoGetDto
{
    public CampoGetDto()
    {
    }
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string NomeUnico { get; set; }
    public bool IsOpcional { get; set; }
    public Guid TipoId { get; set; }
    public LookupDto TipoCampo { get; set; }
}
