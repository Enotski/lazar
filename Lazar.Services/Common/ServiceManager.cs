using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Administration;
using Lazar.Services.Logging;
using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Srevices.Iterfaces.Common;
using Lazar.Srevices.Iterfaces.Logging;

namespace Lazar.Services.Common {
    public class ServiceManager : IServiceManager {
        private Lazy<IRoleService> _lazyRoleService;
        public IRoleService RoleService => _lazyRoleService.Value;

        private Lazy<IUsersService> _lazyUsersService;
        public IUsersService UsersService => _lazyUsersService.Value;

        private Lazy<ILoggingervice> _lazyLoggingervice;
        public ILoggingervice LoggingService => _lazyLoggingervice.Value;

        public ServiceManager(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _lazyRoleService = new Lazy<IRoleService>(() => new RoleService(repositoryManager, mapper));
            _lazyUsersService = new Lazy<IUsersService>(() => new UsersService(repositoryManager, mapper));
            _lazyLoggingervice = new Lazy<ILoggingervice>(() => new Loggingervice(repositoryManager, mapper));
        }
    }
}
