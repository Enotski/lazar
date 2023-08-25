namespace Lazar.Infrastructure.JwtAuth.Models {
    public class LogInRequestDto {
        public string Login { get; set; }
        public string Password { get; set; }
        public LogInRequestDto() { }
        public LogInRequestDto(string login, string password) {
            Login = login;
            Password = password;
        }
    }
}
