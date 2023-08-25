using Lazar.Infrastructure.JwtAuth.Helpers;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Lazar.Infrastructure.JwtAuth.Repositories {
    /// <summary>
    /// Repository for operations with authentication tokens
    /// </summary>
    public class TokenRepository : ITokenRepository  {
        public TokenRepository() { }
        /// <summary>
        /// Crate access token fo authentication model
        /// </summary>
        /// <param name="claims">Claims of user</param>
        /// <param name="issuer">Token issuer</param>
        /// <param name="audience">Token audience</param>
        /// <param name="key">Secret key</param>
        /// <param name="expirationTime">Expiration time of a token</param>
        /// <returns>Generated token</returns>
        public string GenerateAccessToken(IEnumerable<Claim> claims, string issuer, string audience, string key, int expirationTime = 1) {
            var tokeOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: new SigningCredentials(AuthHelper.GetSymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
        /// <summary>
        /// Create refresh token
        /// </summary>
        /// <returns>Generated refresh token</returns>
        public string GenerateRefreshToken() {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        /// <summary>
        /// Get claims from token
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="key">Secret key</param>
        /// <returns>Claims</returns>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string key) {
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthHelper.GetSymmetricSecurityKey(key),
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
