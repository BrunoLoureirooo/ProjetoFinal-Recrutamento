using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.FilterState;

public class CandidaturaFilterStateService
{
    public string? CandidatoNome { get; set; }
    public string? RecrutamentoNome { get; set; }
    public EmpresaDto Empresa { get; set; }
    public LookupDto Estado { get; set; }
}
