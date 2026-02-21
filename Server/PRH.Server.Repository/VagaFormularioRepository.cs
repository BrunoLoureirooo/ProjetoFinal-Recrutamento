using Microsoft.EntityFrameworkCore;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Repository;

internal sealed class VagaFormularioRepository : RepositoryBase<VagaFormulario> , IVagaFormularioRepository
{
    public VagaFormularioRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {

    }

    public async Task CreateVagaFormularioAsync(VagaFormulario vagaFormulario, bool trackChanges)
    {
        Create(vagaFormulario);
        await RepositoryContext.SaveChangesAsync();
    }

}
