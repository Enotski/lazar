namespace Lazar.Infrastructure.JwtAuth.Models.Dto {
    public class AuthDto {
        public string AccessToken { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public AuthDto() { }
        public AuthDto(string accessToken, string issuer, string audience) {
            AccessToken = accessToken;
            Issuer = issuer;
            Audience = audience;
        }
    }
}
