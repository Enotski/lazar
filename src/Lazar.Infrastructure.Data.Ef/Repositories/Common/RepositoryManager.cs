using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.Logging;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Repositories.Logging;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Common {
    /// <summary>
    /// Main repository manager
    /// </summary>
    public class RepositoryManager : IRepositoryManager {
        /// <summary>
        /// Roles repository
        /// </summary>
        public IRolesRepository RoleRepository => _lazyRoleRepository.Value;
        private Lazy<IRolesRepository> _lazyRoleRepository;
        /// <summary>
        /// Users repository
        /// </summary>
        public IUsersRepository UserRepository => _lazyUsersRepository.Value;
        private Lazy<IUsersRepository> _lazyUsersRepository;
        /// <summary>
        /// System logs repository
        /// </summary>
        public ISystemLogRepository SystemLogRepository => _lazyLoggingRepository.Value;
        private Lazy<ISystemLogRepository> _lazyLoggingRepository;

        public RepositoryManager(LazarContext context) {
            _lazyRoleRepository = new Lazy<IRolesRepository>(() => new RolesRepository(context));
            _lazyUsersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(context));
            _lazyLoggingRepository = new Lazy<ISystemLogRepository>(() => new SystemLogRepository(context));
        }
    }
}
