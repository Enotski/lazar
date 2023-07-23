using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.EventLog;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Repositories.Auth;
using lazarData.Repositories.Administration;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Common
{
    public class RepositoryManager : IRepositoryManager
    {
        private Lazy<IRoleRepository> _lazyRoleRepository;
        public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

        private Lazy<IUserProfileRepository> _lazyUserProfileRepository;
        public IUserProfileRepository UserProfileRepository => _lazyUserProfileRepository.Value;

        private Lazy<IUsersRepository> _lazyUsersRepository;
        public IUsersRepository UsersRepository => _lazyUsersRepository.Value;

        private Lazy<IAuthRepository> _lazyAuthRepository;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;

        private Lazy<IEventLogRepository> _lazyEventLogRepository;
        public IEventLogRepository EventLogRepository => _lazyEventLogRepository.Value;

        public RepositoryManager(LazarContext context) {
            _lazyRoleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _lazyUserProfileRepository = new Lazy<IUserProfileRepository>(() => new UserProfileRepository(context));
            _lazyUsersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(context));
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthRepository(context));
            _lazyEventLogRepository = new Lazy<IEventLogRepository>(() => new EventLogRepository(context));
        }
    }
}
