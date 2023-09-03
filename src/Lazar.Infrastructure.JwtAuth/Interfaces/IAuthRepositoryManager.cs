namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    /// <summary>
    /// Repository manager of auth repositories
    /// </summary>
    public interface IAuthRepositoryManager {
        /// <summary>
        /// Authentication repository
        /// </summary>
        IAuthRepository AuthRepository { get; }
        /// <summary>
        /// Repository for operations with authentication tokens
        /// </summary>
        ITokenRepository TokenRepository { get; }
    }
}
