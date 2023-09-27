using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Administration;
using Lazar.Services.Logging;
using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Srevices.Iterfaces.Common;
using Lazar.Srevices.Iterfaces.Logging;

namespace Lazar.Services.Common {
    /// <summary>
    /// Manager of services
    /// </summary>
    public class ServiceManager : IServiceManager {
        /// <summary>
        /// Service of roles
        /// </summary>
        public IRolesService RoleService => _lazyRoleService.Value;
        private Lazy<IRolesService> _lazyRoleService;
        /// <summary>
        /// Service of users
        /// </summary>
        public IUsersService UsersService => _lazyUsersService.Value;
        private Lazy<IUsersService> _lazyUsersService;
        /// <summary>
        /// Service of system events logging
        /// </summary>
        public ISystemLogService LoggingService => _lazyLoggingervice.Value;
        private Lazy<ISystemLogService> _lazyLoggingervice;

        public ServiceManager(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _lazyRoleService = new Lazy<IRolesService>(() => new RolesService(repositoryManager, mapper));
            _lazyUsersService = new Lazy<IUsersService>(() => new UsersService(repositoryManager, mapper));
            _lazyLoggingervice = new Lazy<ISystemLogService>(() => new SystemLogService(repositoryManager, mapper));
        }
    }
}
