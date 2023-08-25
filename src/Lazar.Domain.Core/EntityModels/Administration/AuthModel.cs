using Lazar.Domain.Core.EntityModels.Base;
using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.Models.Administration {
    /// <summary>
    /// Authentication model
    /// </summary>
    public class AuthModel : BaseEntity, ILogin {
        public string Login { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public AuthModel(string login, string? refreshToken, DateTime refreshTokenExpiryTime) : base(Guid.NewGuid()) {
            Login = login;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
        public void Update(string? refreshToken, DateTime refreshTokenExpiryTime) {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
        public void Update(string login, string? refreshToken, DateTime refreshTokenExpiryTime) {
            Login = login;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
    }
}
