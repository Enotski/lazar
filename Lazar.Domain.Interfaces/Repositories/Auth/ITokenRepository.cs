﻿using System.Security.Claims;

namespace Lazar.Domain.Interfaces.Repositories.Auth {
    public interface ITokenRepository {
        string GenerateAccessToken(IEnumerable<Claim> claims, string issuer, string audience, string key);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string key);
    }
}
