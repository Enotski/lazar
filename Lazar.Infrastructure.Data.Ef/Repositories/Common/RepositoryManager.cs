using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.Logging;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Repositories.Auth;
using Lazar.Infrastructure.Data.Ef.Repositories.Logging;
using Lazar.Infrastructure.JwtAuth.Repositories;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Common {
    public class RepositoryManager : IRepositoryManager
    {
        private Lazy<IRoleRepository> _lazyRoleRepository;
        public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

        private Lazy<IUserRepository> _lazyUsersRepository;
        public IUserRepository UserRepository => _lazyUsersRepository.Value;

        private Lazy<ITokenRepository> _lazyTokenRepository;
        public ITokenRepository TokenRepository => _lazyTokenRepository.Value;

        private Lazy<IAuthRepository> _lazyAuthRepository;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;

        private Lazy<ISystemLogRepository> _lazyLoggingRepository;
        public ISystemLogRepository SystemLogRepository => _lazyLoggingRepository.Value;

        public RepositoryManager(LazarContext context) {
            _lazyRoleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _lazyUsersRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _lazyTokenRepository = new Lazy<ITokenRepository>(() => new TokenRepository());
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthReppository(context));
            _lazyLoggingRepository = new Lazy<ISystemLogRepository>(() => new SystemLogRepository(context));
        }
    }
}
