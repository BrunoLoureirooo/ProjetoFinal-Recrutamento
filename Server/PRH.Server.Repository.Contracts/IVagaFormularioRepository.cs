using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface IVagaFormularioRepository
{
    Task CreateVagaFormularioAsync(VagaFormulario vagaFormulario, bool trackChanges);
}
