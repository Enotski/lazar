namespace Lazar.Infrastructure.JwtAuth.Models.Dto {
    public sealed class TokensDto : AuthDto {
        public string RefreshToken { get; set; }
        public TokensDto() : base() { }
        public TokensDto(string accessToken, string refreshKey, string issuer, string audience) : base(accessToken, issuer, audience) {
            RefreshToken = refreshKey;
        }
    }
}
