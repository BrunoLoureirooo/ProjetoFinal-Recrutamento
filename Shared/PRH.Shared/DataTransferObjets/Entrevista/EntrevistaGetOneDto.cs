using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Funcionario;

namespace PRH.Shared.DataTransferObjets.Entrevista;

public class EntrevistaGetOneDto
{
    public EntrevistaGetOneDto()
    {
    }

    public Guid Id { get; set; }
    public CandidaturaGetOneEntrevistaDto Candidatura { get; set; }
    public FuncionarioGetOneEntrevistaDto Entrevistador { get; set; }
    public string Notas { get; set; }
    public DateTime DataHora { get; set; }
    public LookupDto Resultado { get; set; }
}
