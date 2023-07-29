namespace Lazar.Infrastructure.JwtAuth.Models {
    public sealed class UserAuthDto {
        public TokensDto Tokens { get; set; }
        public string Login { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public UserAuthDto() { }
        public UserAuthDto(TokensDto tokens, string login, IEnumerable<string> roles) {
            Tokens = tokens;
            Login = login;
            Roles = roles;
        }
    }
}
