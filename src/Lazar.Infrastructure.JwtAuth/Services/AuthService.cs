using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Models;
using Lazar.Infrastructure.JwtAuth.Models.Dto;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Lazar.Infrastructure.JwtAuth.Services {
    /// <summary>
    /// Authentication service
    /// </summary>
    public class AuthService : IAuthService {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IAuthRepositoryManager _authRepositoryManager;

        private readonly AuthConfiguration _configuration;
        public AuthService(IRepositoryManager repositoryManager, IAuthRepositoryManager authRepositoryManager, IOptions<AuthConfiguration> options) {
            _repositoryManager = repositoryManager;
            _authRepositoryManager = authRepositoryManager;
            _configuration = options.Value;
        }
        private record TokenOptions(string AccessToken, string RefreshToken, DateTime ExpiredTime);

        /// <summary>
        /// Claims of user
        /// </summary>
        /// <param name="user">User in system</param>
        /// <returns>Claims (login and roles)</returns>
        private List<Claim> GetClaims(UserSelectorModel user) {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Login),
                };
            foreach (var role in user.RoleNames) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        /// <summary>
        /// Tokens
        /// </summary>
        /// <param name="claims">Claims of user</param>
        /// <returns>Access and refresh tokens</returns>
        private TokenOptions GetTokenOptions(List<Claim> claims) {
            var expiredTime = DateTime.Now.AddMinutes(1);
            var accessToken = _authRepositoryManager.TokenRepository.GenerateAccessToken(claims, _configuration.Issuer, _configuration.Audience, _configuration.Key, expiredTime);
            var refreshToken = _authRepositoryManager.TokenRepository.GenerateRefreshToken();

            return new TokenOptions(accessToken, refreshToken, expiredTime);
        }
        /// <summary>
        /// Login to system
        /// </summary>
        /// <param name="model">Login reques model</param>
        /// <returns>User authentication model with generated tokens</returns>
        public async Task<UserAuthDto> LogInAsync(LogInRequestDto model) {
            try {
                if (model is null) {
                    throw new Exception("Invalid client request");
                }
                var isExist = await _repositoryManager.UserRepository.LoginExistsAsync(model.Login);
                if (!isExist)
                    throw new Exception("User not found");
                var passwordHelper = new PasswordHelper();
                var user = await _repositoryManager.UserRepository.GetByLoginAsync(model.Login);
                if (!passwordHelper.VerifyPasword(model.Password, user.Password))
                    throw new Exception("Invalid password");

                var claims = GetClaims(user);

                var tokenOptions = GetTokenOptions(claims);

                var loginModel = await _authRepositoryManager.AuthRepository.GetAuthModelAsync(user.Login);
                if (loginModel == null) {
                    loginModel = new AuthModel(user.Login, tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                    await _authRepositoryManager.AuthRepository.AddAsync(loginModel);
                } else {
                    loginModel.Update(tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                    await _authRepositoryManager.AuthRepository.UpdateAsync(loginModel);
                }

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.LogIn, "", $"{user.Login}");

                return new UserAuthDto(new TokensDto(tokenOptions.AccessToken, tokenOptions.RefreshToken, _configuration.Issuer, _configuration.Audience), user.Login, user.RoleNames);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Login attempt", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Register new user in system
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <returns>User authentication model with generated tokens</returns>
        public async Task<UserAuthDto> RegisterAsync(UserRegisterRequestDto model) {
            try {
                if (model is null) {
                    throw new Exception("Invalid client request");
                }
                var exist = await _repositoryManager.UserRepository.LoginExistsAsync(model.Login);
                if (exist)
                    throw new Exception("Login already exist");

                await _repositoryManager.UserRepository.AddAsync(new User(model.Login, model.Login, model.Password, model.Email, ""));

                var user = await _repositoryManager.UserRepository.GetByLoginAsync(model.Login);
                if (user is null)
                    throw new Exception("User not found");

                var claims = GetClaims(user);

                var tokenOptions = GetTokenOptions(claims);

                var loginModel = new AuthModel(user.Login, tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                await _authRepositoryManager.AuthRepository.AddAsync(loginModel);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Registration, "", $"{user.Login}");

                return new UserAuthDto(new TokensDto(tokenOptions.AccessToken, tokenOptions.RefreshToken, _configuration.Issuer, _configuration.Audience), user.Login, user.RoleNames);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Register attempt", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// LogOut from system
        /// </summary>
        /// <param name="model">Logout model</param>
        /// <returns></returns>
        public async Task LogOutAsync(LogOutRequestDto model) {
            try {
                var loginModel = await _authRepositoryManager.AuthRepository.GetAuthModelAsync(model.Login);
                if (loginModel != null) {
                    await _authRepositoryManager.AuthRepository.DeleteAsync(loginModel);
                }
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Logout attempt", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Update token of authenticated user
        /// </summary>
        /// <param name="model">Authentication model with tokens information</param>
        /// <returns>User authentication model with generated tokens</returns>
        public async Task<UserAuthDto> RefreshTokenAsync(TokensDto model) {
            try {
                if (model is null)
                    throw new Exception("Invalid client request");

                var principal = _authRepositoryManager.TokenRepository.GetPrincipalFromExpiredToken(model.AccessToken, _configuration.Key);
                var username = principal.Identity.Name; //this is mapped to the Name claim by default

                var loginModel = await _authRepositoryManager.AuthRepository.GetAuthModelAsync(username);
                if (loginModel is null || loginModel.RefreshToken != model.RefreshToken)
                    throw new Exception("Invalid client request");

                var user = await _repositoryManager.UserRepository.GetByLoginAsync(loginModel.Login);

                var tokenOptions = GetTokenOptions(GetClaims(user));
                loginModel.Update(tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                await _authRepositoryManager.AuthRepository.UpdateAsync(loginModel);

                return new UserAuthDto(new TokensDto(tokenOptions.AccessToken, tokenOptions.RefreshToken, _configuration.Issuer, _configuration.Audience), loginModel.Login, user.RoleNames);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Token refresh attempt", exp.Format());
                throw;
            }
        }
    }
}
