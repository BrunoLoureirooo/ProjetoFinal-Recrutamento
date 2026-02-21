namespace PRH.Shared.DataTransferObjets.Cargo;

public class CargoGetAllDto
{
    public CargoGetAllDto()
    {
    }
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public short NumDocumentosNecessarios { get; set; }
}
