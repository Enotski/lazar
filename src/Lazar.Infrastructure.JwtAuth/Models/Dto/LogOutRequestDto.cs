namespace Lazar.Infrastructure.JwtAuth.Models.Dto {
    public class LogOutRequestDto {
        public string Login { get; set; }
        public LogOutRequestDto() { }
        public LogOutRequestDto(string login) {
            Login = login;
        }
    }
}
