using Lazar.Infrastructure.JwtAuth.Models.Dto;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public interface IAuthService {
        /// <summary>
        /// Login to system
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns>User authentication model with generated tokens</returns>
        Task<UserAuthDto> LogInAsync(LogInRequestDto model);
        /// <summary>
        /// LogOut from system
        /// </summary>
        /// <param name="model">Logout model</param>
        /// <returns></returns>
        Task LogOutAsync(LogOutRequestDto model);
        /// <summary>
        /// Register new user in system
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <returns>User authentication model with generated tokens</returns>
        Task<UserAuthDto> RegisterAsync(UserRegisterRequestDto model);
        /// <summary>
        /// Update token of authenticated user
        /// </summary>
        /// <param name="model">Authentication model with tokens information</param>
        /// <returns>User authentication model with generated tokens</returns>
        Task<UserAuthDto> RefreshTokenAsync(TokensDto model);
    }
}
