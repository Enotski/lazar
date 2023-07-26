using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.EventLogs;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Repositories.Auth;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Common {
    public class RepositoryManager : IRepositoryManager
    {
        private Lazy<IRoleRepository> _lazyRoleRepository;
        public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

        private Lazy<IUserRepository> _lazyUsersRepository;
        public IUserRepository UserRepository => _lazyUsersRepository.Value;

        private Lazy<IAuthRepository> _lazyAuthRepository;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;

        private Lazy<ISystemLogRepository> _lazyEventLogRepository;
        public ISystemLogRepository SystemLogRepository => _lazyEventLogRepository.Value;

        public RepositoryManager(LazarContext context) {
            _lazyRoleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _lazyUsersRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthRepository(context));
            _lazyEventLogRepository = new Lazy<ISystemLogRepository>(() => new SystemLogRepository(context));
        }
    }
}
