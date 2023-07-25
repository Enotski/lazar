using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.EventLog;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Repositories.Auth;
using Lazar.Domain.Core.Repositories.Administration;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Common
{
    public class RepositoryManager : IRepositoryManager
    {
        private Lazy<IRoleRepository> _lazyRoleRepository;
        public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

        private Lazy<IUsersRepository> _lazyUsersRepository;
        public IUsersRepository UsersRepository => _lazyUsersRepository.Value;

        private Lazy<IAuthRepository> _lazyAuthRepository;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;

        private Lazy<ISystemLogRepository> _lazyEventLogRepository;
        public ISystemLogRepository EventLogRepository => _lazyEventLogRepository.Value;

        public RepositoryManager(LazarContext context) {
            _lazyRoleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _lazyUsersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(context));
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthRepository(context));
            _lazyEventLogRepository = new Lazy<ISystemLogRepository>(() => new EventLogRepository(context));
        }
    }
}
