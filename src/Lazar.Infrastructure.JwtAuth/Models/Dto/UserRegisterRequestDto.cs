namespace Lazar.Infrastructure.JwtAuth.Models.Dto {
    public sealed class UserRegisterRequestDto : LogInRequestDto {
        public string Email { get; set; }
        public UserRegisterRequestDto() { }
        public UserRegisterRequestDto(string login, string password, string email) : base(login, password) {
            Email = email;
        }
    }
}
