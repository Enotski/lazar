using Lazar.Infrastructure.JwtAuth.Models;
using System.Security.Claims;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    public interface IAuthService {
        Task<UserAuthDto> LogInAsync(LogInRequestDto model);
        Task<bool> LogOutAsync(string login);
        Task<UserAuthDto> SignUpAsync(SignUpRequestDto model);
        Task<UserAuthDto> RefreshTokenAsync(TokensDto model);
    }
}
