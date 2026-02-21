using PRH.Shared.DataTransferObjets.Vaga;
using System.Collections.ObjectModel;
using PRH.Shared.DataTransferObjets.CampoRespondido;
using PRH.Shared.DataTransferObjets.Entrevista;

namespace PRH.Shared.DataTransferObjets.Candidatura;

public class CandidaturaGetOneDto
{
    public CandidaturaGetOneDto()
    {
    }

    public Guid Id { get; set; }
    public int NIFcandidato { get; set; }
    public string NomeCandidato { get; set; }
    public int ContactoCandidato { get; set; }
    public string EmailCandidato { get; set; }
    public string RecrutamentoNome { get; set; }
    public EmpresaDto Empresa { get; set; }
    public VagaGetAllDto Vaga { get; set; }
    public LookupDto Estado { get; set; }
    public Collection<CampoRespondidoGetOneCandidaturaDto> CamposRespondidos { get; set; }
    public Collection<EntrevistaGetAllDto> Entrevistas { get; set; }
}
