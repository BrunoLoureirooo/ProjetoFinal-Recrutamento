using AutoMapper;
using PRH.Server.Entities.ConfigurationModels;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PRH.Server.Contracts;
using PRH.Server.Repository.Contracts;

namespace PRH.Server.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmpresaService> _empresaService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, 
            //IDataShaper<EmployeeDto> dataShaper,
            UserManager<ApplicationUser> userManager,
            IOptions<JwtConfiguration> configuration)
        {
            _empresaService = new Lazy<IEmpresaService>(() => new EmpresaService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() =>new AuthenticationService(logger, mapper, userManager, configuration));
        }
        public IEmpresaService EmpresaService => _empresaService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
