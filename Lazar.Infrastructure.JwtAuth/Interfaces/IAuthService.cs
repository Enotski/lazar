using Lazar.Infrastructure.JwtAuth.Models;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    public interface IAuthService {
        Task<UserAuthDto> LogInAsync(LogInRequestDto model);
        Task LogOutAsync(string login);
        Task<UserAuthDto> SignUpAsync(SignUpRequestDto model);
        Task<UserAuthDto> RefreshTokenAsync(TokensDto model);
    }
}
