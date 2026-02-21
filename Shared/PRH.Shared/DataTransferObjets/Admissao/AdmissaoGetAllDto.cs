using PRH.Shared.DataTransferObjets.Funcionario;

namespace PRH.Shared.DataTransferObjets.Admissao;

public class AdmissaoGetAllDto
{
    public AdmissaoGetAllDto()
    {
    }

    public Guid Id { get; set; }
    public string NomeCandidato { get; set; }
    public int NIFcandidato { get; set; }
    public int ContactoCandidato { get; set; }
    public string EmailCandidato { get; set; }
    public string RecrutamentoNome { get; set; }
}
