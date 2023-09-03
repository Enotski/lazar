using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Repositories;

namespace Lazar.Infrastructure.JwtAuth.Common.Auth {
    /// <summary>
    /// Repository manager of auth repositories
    /// </summary>
    public class AuthRepositoryManager : IAuthRepositoryManager {
        /// <summary>
        /// Repository for operations with authentication tokens
        /// </summary>
        public ITokenRepository TokenRepository => _lazyTokenRepository.Value;
        private Lazy<ITokenRepository> _lazyTokenRepository;
        /// <summary>
        /// Authentication repository
        /// </summary>
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;
        private Lazy<IAuthRepository> _lazyAuthRepository;

        public AuthRepositoryManager(LazarContext context) {
            _lazyTokenRepository = new Lazy<ITokenRepository>(() => new TokenRepository());
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthReppository(context));
        }
    }
}
