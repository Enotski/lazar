using Lazar.Domain.Core.EntityModels.Base;

namespace Lazar.Domain.Core.EntityModels.Auth
{
    public class LoginModel : BaseEntity
    {
        public string? Login { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public LoginModel() : base() { }
        public LoginModel(string? login, string? refreshToken, DateTime refreshTokenExpiryTime) : base(Guid.NewGuid()){
            Login = login;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
        public void Update(string? refreshToken, DateTime refreshTokenExpiryTime) {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
        public void Update(string? login, string? refreshToken, DateTime refreshTokenExpiryTime) {
            Login = login;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
    }
}
