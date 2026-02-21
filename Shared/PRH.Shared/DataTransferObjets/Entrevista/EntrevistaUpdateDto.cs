using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Funcionario;

namespace PRH.Shared.DataTransferObjets.Entrevista;

public class EntrevistaUpdateDto
{
    public EntrevistaUpdateDto()
    {
    }

    public Guid? EntrevistadorId { get; set; }
    public string? Notas { get; set; }
    public DateTime? DataHora { get; set; }
    public Guid? ResultadoId { get; set; }
}
