using PRH.Server.Entities.Models;
using PRH.Shared.DataTransferObjets.Campo;

namespace PRH.Server.Service.Contracts;
public interface ICampoService
{
    public Task<IEnumerable<CampoGetDto>> GetCamposByVaga(Guid vagaId, bool trackChanges);
    public Task<CampoGetDto> CreateCampoAsync(CampoCreateDto dto, bool trackChanges);
    public Task<IEnumerable<CampoGetDto>> GetCampos(bool trackChanges);

}
