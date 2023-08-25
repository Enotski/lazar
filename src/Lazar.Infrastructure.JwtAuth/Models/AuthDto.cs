namespace Lazar.Infrastructure.JwtAuth.Models {
    public class AuthDto {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public AuthDto() { }
        public AuthDto(string key, string issuer, string audience) {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }
    }
}
