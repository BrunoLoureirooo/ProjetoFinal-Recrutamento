using PRH.Server.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Repository.Contracts.Lookups;

public interface ITipoFormularioRepository
{
    Task<IEnumerable<TipoFormulario>> GetTiposFormularioAsync(bool trackChanges);
}
