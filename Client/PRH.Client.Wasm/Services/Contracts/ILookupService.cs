using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.Contracts;

public interface ILookupService
{
    Task<IEnumerable<LookupDto>> GetModalidadesAsync();
    Task<IEnumerable<LookupDto>> GetTiposVagaAsync();
    Task<IEnumerable<LookupDto>> GetTiposCampoAsync();
    Task<LookupDto> GetTipoCampoByIdAsync(Guid tipoCampoId);
    Task<IEnumerable<LookupDto>> GetTiposFormularioAsync();
    Task<IEnumerable<LookupDto>> GetEstadosAsync();
    Task<IEnumerable<LookupDto>> GetResultadosAsync();
    //Task<IEnumerable<LookupDto>> GetLookupsAsync();
    //Task<IEnumerable<LookupDto>> GetLookupByTipoAsync(string Tipo);
}
