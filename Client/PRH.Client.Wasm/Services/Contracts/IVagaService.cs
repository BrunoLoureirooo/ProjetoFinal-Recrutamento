using PRH.Shared.DataTransferObjets.Vaga;

namespace PRH.Client.Wasm.Services.Contracts;

public interface IVagaService
{
    Task<VagaGetOneDto> GetVagaByIdAsync(Guid id);
    Task CreateVagaAsync(VagaCreateDto dto);
    Task UpdateVagaAsync(Guid id, VagaUpdateDto dto);
}
