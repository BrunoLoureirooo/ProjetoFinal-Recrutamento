using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Contracts;

public interface IFormularioRespondidoRepository
{
	Task<IEnumerable<FormularioRespondido>> GetRespostasByCandidaturaAsync(Guid candidaturaId, bool trackChanges);
}
