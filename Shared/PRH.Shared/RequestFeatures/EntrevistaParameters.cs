namespace PRH.Shared.RequestFeatures;

public class EntrevistaParameters : RequestParameters
{
    public string? NomeCandidato { get; set; }
    public Guid? ResultadoId { get; set; }
    public DateTime? Before { get; set; }
    public DateTime? After { get; set; }
    public Guid? EmpresaId { get; set; }

    public bool ValidDates => Before > After;
}
