using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    /// <summary>
    /// Repository for operations with authentication tokens
    /// </summary>
    public interface ITokenRepository {
        /// <summary>
        /// Crate access token fo authentication model
        /// </summary>
        /// <param name="claims">Claims of user</param>
        /// <param name="issuer">Token issuer</param>
        /// <param name="audience">Token audience</param>
        /// <param name="key">Secret key</param>
        /// <param name="expires">Expiration time of a token</param>
        /// <returns>Generated token</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims, string issuer, string audience, string key, DateTime expires);
        /// <summary>
        /// Create refresh token
        /// </summary>
        /// <returns>Generated refresh token</returns>
        string GenerateRefreshToken();
        /// <summary>
        /// Get claims from token
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="key">Secret key</param>
        /// <returns>Claims</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string key);
    }
}
