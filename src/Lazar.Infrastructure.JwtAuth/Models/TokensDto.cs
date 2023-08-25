namespace Lazar.Infrastructure.JwtAuth.Models {
    public sealed class TokensDto : AuthDto
    {
        public string RefreshKey { get; set; }
        public TokensDto() : base() { }
        public TokensDto(string key, string refreshKey, string issuer, string audience) : base(key, issuer, audience) {
            RefreshKey = refreshKey;
        }
    }
}
