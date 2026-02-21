namespace PRH.Shared.RequestFeatures;

public class CandidaturaParameters : RequestParameters
{
    public string? CandidatoNome { get; set; }
    public string? RecrutamentoNome { get; set; }
    public Guid? EmpresaId { get; set; }
    public Guid? EstadoId { get; set; }
}
