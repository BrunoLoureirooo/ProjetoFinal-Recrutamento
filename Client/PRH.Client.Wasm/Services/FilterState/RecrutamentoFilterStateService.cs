using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.FilterState;

public class RecrutamentoFilterStateService
{
    public string? Nome { get; set; }
    public EmpresaDto Empresa { get; set; }
    public DateTime? After { get; set; }
    public DateTime? Before { get; set; }
    public bool? CandDisponiveis { get; set; }
}
