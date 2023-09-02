namespace Lazar.Infrastructure.JwtAuth.Models.Dto {
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
