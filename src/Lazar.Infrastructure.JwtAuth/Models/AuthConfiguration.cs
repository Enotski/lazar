namespace Lazar.Infrastructure.JwtAuth.Models {
    public class AuthConfiguration {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public AuthConfiguration() { }
        public AuthConfiguration(string key, string issuer, string audience) {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }
    }
}
