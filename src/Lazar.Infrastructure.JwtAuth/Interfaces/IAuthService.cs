using Lazar.Infrastructure.JwtAuth.Models;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    /// <summary>
    /// Authentication service
    /// </summary>
    public interface IAuthService {
        /// <summary>
        /// Login to system
        /// </summary>
        /// <param name="model">Login reques model</param>
        /// <returns>User authentication model with generated tokens</returns>
        Task<UserAuthDto> LogInAsync(LogInRequestDto model);
        /// <summary>
        /// LogOut from system
        /// </summary>
        /// <param name="login">Login of authentication model</param>
        /// <returns></returns>
        Task LogOutAsync(string login);
        /// <summary>
        /// Register new user in system
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <returns>User authentication model with generated tokens</returns>
        Task<UserAuthDto> RegisterAsync(SignUpRequestDto model);
        /// <summary>
        /// Update token of authenticated user
        /// </summary>
        /// <param name="model">Authentication model with tokens information</param>
        /// <returns>User authentication model with generated tokens</returns>
        Task<UserAuthDto> RefreshTokenAsync(TokensDto model);
    }
}
