namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaGetOneVagaDto
{
    public CandidaturaGetOneVagaDto()
    {
    }

    public Guid Id { get; set; }
    public int NIFcandidato { get; set; }
    public string NomeCandidato { get; set; }
    public int ContactoCandidato { get; set; }
    public string EmailCandidato { get; set; }
    public LookupDto Estado { get; set; }
}
