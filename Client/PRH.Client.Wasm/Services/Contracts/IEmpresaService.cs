using PRH.Shared.DataTransferObjets;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaDto>> GetEmpresasAsync();
    Task<EmpresaDto> GetMyEmpresaAsync();
    Task<IEnumerable<EmpresaDto>> GetEmpresasByNameAsync(string nome);
}
