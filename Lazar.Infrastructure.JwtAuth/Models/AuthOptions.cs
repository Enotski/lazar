using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lazar.Infrastructure.JwtAuth.Models {
    public class AuthOptions {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string KEY) =>
           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
