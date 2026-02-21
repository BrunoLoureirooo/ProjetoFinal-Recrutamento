namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaGetAllDto
{
    public CandidaturaGetAllDto()
    {
    }

    public Guid Id { get; set; }
    public int NIFcandidato { get; set; }
    public string NomeCandidato { get; set; }
    public int ContactoCandidato { get; set; }
    public string EmailCandidato { get; set; }
    public string EmpresaNome { get; set; }
    public string RecrutamentoNome { get; set; }
    public LookupDto Estado { get; set; }
}
