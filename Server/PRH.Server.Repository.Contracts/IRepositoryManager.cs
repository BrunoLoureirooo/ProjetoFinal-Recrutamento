using System;

namespace PRH.Server.Repository.Contracts;

public interface IRepositoryManager
{
    IEmpresaRepository Empresa { get; }
    Task SaveAsync();
}

