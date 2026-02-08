using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Service.Contracts
{
    public interface IServiceManager
    {
        IEmpresaService EmpresaService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
