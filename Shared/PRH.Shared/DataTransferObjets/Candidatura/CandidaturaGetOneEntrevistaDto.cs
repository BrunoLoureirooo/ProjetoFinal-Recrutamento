namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaGetOneEntrevistaDto
{
    public CandidaturaGetOneEntrevistaDto()
    {
    }
    public Guid Id { get; set; }
    public int NIFcandidato { get; set; }
    public string NomeCandidato { get; set; }
    public int ContactoCandidato { get; set; }
    public string EmailCandidato { get; set; }
    public Guid RecrutamentoId { get; set; }
    public string Estado { get; set; }
}
