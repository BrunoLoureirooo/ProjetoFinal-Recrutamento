using PRH.Shared.DataTransferObjets.Formulario;

namespace PRH.Server.Service.Contracts;

public interface IFormularioRespondidoService
{
    Task<IEnumerable<FormularioRespondidoDto>> GetRespostasByCandidatura(Guid candidaturaId, bool trackChanges);
}
