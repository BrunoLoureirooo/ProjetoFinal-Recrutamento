using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.FilterState;

public class AdmissaoFilterStateService
{
    public string? NomeCandidato { get; set; }
    public string? NomeRecrutamento { get; set; }
    public EmpresaDto Empresa { get; set; }
}
