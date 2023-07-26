using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Administration;
using Lazar.Services.Auth;
using Lazar.Services.EventLog;
using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Srevices.Iterfaces.Auth;
using Lazar.Srevices.Iterfaces.Common;
using Lazar.Srevices.Iterfaces.EventLog;

namespace Lazar.Services.Common
{
    public class ServiceManager : IServiceManager
    {
        private Lazy<IRoleService> _lazyRoleService;
        public IRoleService RoleService => _lazyRoleService.Value;

        private Lazy<IUsersService> _lazyUsersService;
        public IUsersService UsersService => _lazyUsersService.Value;

        private Lazy<IAuthService> _lazyAuthService;
        public IAuthService AuthService => _lazyAuthService.Value;

        private Lazy<IEventLogService> _lazyEventLogService;
        public IEventLogService EventLogService => _lazyEventLogService.Value;

        public ServiceManager(IRepositoryManager repositoryManager, IModelMapper mapper)
        {
            _lazyRoleService = new Lazy<IRoleService>(() => new RoleService(repositoryManager, mapper));
            _lazyUsersService = new Lazy<IUsersService>(() => new UsersService(repositoryManager, mapper));
            _lazyAuthService = new Lazy<IAuthService>(() => new AuthService(repositoryManager, mapper));
            _lazyEventLogService = new Lazy<IEventLogService>(() => new EventLogService(repositoryManager, mapper));
        }
    }
}
