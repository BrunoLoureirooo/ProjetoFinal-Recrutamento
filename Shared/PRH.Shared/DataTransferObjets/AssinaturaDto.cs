namespace PRH.Shared.DataTransferObjets
{
    public class AssinaturaDto
    {
        public Guid documentoId { get; set; } = new Guid();
        public List<string> Assinaturas { get; set; } = new();

    }
}
