using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lazar.Infrastructure.JwtAuth.Helpers {
    /// <summary>
    /// Helper class for auth processes
    /// </summary>
    public static class AuthHelper {
        /// <summary>
        /// Create symmetric key
        /// </summary>
        /// <param name="key">Secret key</param>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}
