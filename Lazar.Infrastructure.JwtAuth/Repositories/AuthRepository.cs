using Lazar.Infrastructure.JwtAuth.Interfaces;
using Lazar.Infrastructure.JwtAuth.Models;
using System.Security.Claims;

namespace Lazar.Infrastructure.JwtAuth.Repositories {
    public class AuthRepository : IAuthRepository  {
        public TokensDto GenerateRefreshToken(string userName) {
            throw new NotImplementedException();
        }

        public Task<TokensDto> GenerateToken(string userName) {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
            throw new NotImplementedException();
        }
    }
}
