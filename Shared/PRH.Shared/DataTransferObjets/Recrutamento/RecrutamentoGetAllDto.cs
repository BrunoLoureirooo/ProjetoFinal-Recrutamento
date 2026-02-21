using System.Collections.ObjectModel;

namespace PRH.Shared.DataTransferObjets.Recrutamento;

public class RecrutamentoGetAllDto
{
    public RecrutamentoGetAllDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public DateTime Data { get; set; }
    public EmpresaDto Empresa { get; set; }
    public int TotalVagas { get; set; }
    public Collection<string> Cargos { get; set; }
}

