using System;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Service.Contracts;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaDto>> GetEmpresas(bool trackChanges);
}
