namespace PRH.Shared.RequestFeatures;

public class AdmissaoParameters : RequestParameters
{
    public string? CandidatoNome { get; set; }
    public string? RecrutamentoNome { get; set; }
    public Guid? EmpresaId { get; set; }
}
