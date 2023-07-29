using Lazar.Infrastructure.JwtAuth.Models;
using System.Security.Claims;

namespace Lazar.Infrastructure.JwtAuth.Interfaces {
    internal interface IAuthRepository {
        Task<TokensDto> GenerateToken(string userName);
        TokensDto GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
