using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Repositories;

namespace Lazar.Infrastructure.JwtAuth.Common.Auth {
    public class AuthRepositoryManager : IAuthRepositoryManager
    {
        private Lazy<ITokenRepository> _lazyTokenRepository;
        public ITokenRepository TokenRepository => _lazyTokenRepository.Value;

        private Lazy<IAuthRepository> _lazyAuthRepository;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;

        public AuthRepositoryManager(LazarContext context) {
            _lazyTokenRepository = new Lazy<ITokenRepository>(() => new TokenRepository());
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthReppository(context));
        }
    }
}
