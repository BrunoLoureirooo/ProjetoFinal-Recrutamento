namespace PRH.Shared.RequestFeatures;

public class RecrutamentoParameters : RequestParameters
{
    public string? Nome { get; set; }
    public Guid? EmpresaId { get; set; }
    public DateTime? Before { get; set; }
    public DateTime? After { get; set; }
    public bool? CandidaturasDisponiveis { get; set; }

    public bool ValidDates => Before > After;
}
