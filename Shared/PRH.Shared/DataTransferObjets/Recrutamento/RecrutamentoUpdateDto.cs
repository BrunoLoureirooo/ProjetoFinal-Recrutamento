namespace PRH.Shared.DataTransferObjets.Recrutamento;

public class RecrutamentoUpdateDto
{
    public RecrutamentoUpdateDto()
    {
    }
    public string? Nome { get; set; }
    public DateTime? Data { get; set; }
    public Guid? EmpresaId { get; set; }
}
