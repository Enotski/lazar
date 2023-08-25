namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    public interface IAuthRepositoryManager {
        IAuthRepository AuthRepository { get; }
        ITokenRepository TokenRepository { get; }
    }
}
