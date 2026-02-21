using PRH.Shared.DataTransferObjets.Funcionario;

namespace PRH.Shared.DataTransferObjets.Entrevista;

public class EntrevistaGetAllDto
{
    public EntrevistaGetAllDto()
    {
    }

    public Guid Id { get; set; }
    public string NomeCandidato { get; set; }
    public DateTime DataHora { get; set; }
    public FuncionarioGetOneEntrevistaDto Entrevistador { get; set; }
    public LookupDto Resultado { get; set; }
}
