namespace Lazar.Infrastructure.JwtAuth.Models {
    public sealed class SignUpRequestDto : LogInRequestDto {
        public string Email { get; set; }
        public SignUpRequestDto() { }
        public SignUpRequestDto(string login, string password, string email) : base(login, password) {
            Email = email;
        }
    }
}
