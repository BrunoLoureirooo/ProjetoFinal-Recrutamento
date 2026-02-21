using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Cargo;

namespace PRH.Client.Wasm.Services.FilterState;

public class VagaFilterStateService
{
    public LookupDto Modalidade;
    public LookupDto TipoVaga;
    public CargoGetAllDto Cargo;
    public bool? CandDisponiveis;
    public string? Descricao;
}
