using CommonUtils.Utils;
using Lazar.Domain.Core.EntityModels.Auth;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Lazar.Infrastructure.JwtAuth.Services {
    public class AuthService : IAuthService {
        private readonly IRepositoryManager _repositoryManager;
        //private readonly IModelMapper _mapper;
        private readonly AuthDto _configuration;
        public AuthService(IRepositoryManager repositoryManager, /*IModelMapper mapper,*/ IOptions<AuthDto> options) {
            _repositoryManager = repositoryManager;
            //_mapper = mapper;
            _configuration = options.Value;
        }

        private record TokenOptions(string AccessToken, string RefreshToken, DateTime ExpiredTime);
        private List<Claim> GetClaims(UserSelectorModel user) {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Login),
                };
            foreach (var role in user.Roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private TokenOptions GetTokenOptions(List<Claim> claims) {
            var accessToken = _repositoryManager.TokenRepository.GenerateAccessToken(claims, _configuration.Issuer, _configuration.Audience, _configuration.Key);
            var refreshToken = _repositoryManager.TokenRepository.GenerateRefreshToken();
            var expiredTime = DateTime.Now.AddDays(7);

            return new TokenOptions(accessToken, refreshToken, expiredTime);
        }

        public async Task<UserAuthDto> LogInAsync(LogInRequestDto model) {
            try {
                if (model is null) {
                    throw new Exception("Invalid client request");
                }
                var isExist = await _repositoryManager.UserRepository.UserLoginExistAsync(model.Login);
                if (!isExist)
                    throw new Exception("User not found");

                var user = await _repositoryManager.UserRepository.GetByLoginAsync(model.Login);
                if (user.Password != model.Password)
                    throw new Exception("Invalid password");

                var claims = GetClaims(user);

                var tokenOptions = GetTokenOptions(claims);

                var loginModel = await _repositoryManager.AuthRepository.GetLoginModelAsync(user.Login);
                if (loginModel == null) {
                    loginModel = new LoginModel(user.Login, tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                    await _repositoryManager.AuthRepository.AddAsync(loginModel);
                } else {
                    loginModel.Update(tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                    await _repositoryManager.AuthRepository.UpdateAsync(loginModel);
                }

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.LogIn, "", $"{user.Login}");

                return new UserAuthDto(new TokensDto(tokenOptions.AccessToken, tokenOptions.RefreshToken, _configuration.Issuer, _configuration.Audience), user.Login, user.Roles);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
        public async Task<UserAuthDto> SignUpAsync(SignUpRequestDto model) {
            try {
                if (model is null) {
                    throw new Exception("Invalid client request");
                }
                var exist = await _repositoryManager.UserRepository.UserLoginExistAsync(model.Login);
                if (exist)
                    throw new Exception("Login already exist");

                await _repositoryManager.UserRepository.AddAsync(new User(model.Login, model.Login, model.Password, model.Email, ""));

                var user = await _repositoryManager.UserRepository.GetByLoginAsync(model.Login);
                if (user is null)
                    throw new Exception("User not found");

                var claims = GetClaims(user);

                var tokenOptions = GetTokenOptions(claims);

                var loginModel = new LoginModel(user.Login, tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                await _repositoryManager.AuthRepository.AddAsync(loginModel);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Registration, "", $"{user.Login}");

                return new UserAuthDto(new TokensDto(tokenOptions.AccessToken, tokenOptions.RefreshToken, _configuration.Issuer, _configuration.Audience), user.Login, user.Roles);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
        public async Task LogOutAsync(string login) {
            try {
                var loginModel = await _repositoryManager.AuthRepository.GetLoginModelAsync(login);
                if (loginModel != null) {
                    await _repositoryManager.AuthRepository.DeleteAsync(loginModel);
                }
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }

        public async Task<UserAuthDto> RefreshTokenAsync(TokensDto model) {
            try {
                if (model is null)
                    throw new Exception("Invalid client request");

                string accessToken = model.Key;
                string refreshToken = model.RefreshKey;
                var principal = _repositoryManager.TokenRepository.GetPrincipalFromExpiredToken(accessToken, _configuration.Key);
                var username = principal.Identity.Name; //this is mapped to the Name claim by default

                var loginModel = await _repositoryManager.AuthRepository.GetLoginModelAsync(username);
                if (loginModel is null || loginModel.RefreshToken != refreshToken || loginModel.RefreshTokenExpiryTime <= DateTime.Now)
                    throw new Exception("Invalid client request");

                var user = await _repositoryManager.UserRepository.GetByLoginAsync(loginModel.Login);

                var tokenOptions = GetTokenOptions(GetClaims(user));
                loginModel.Update(tokenOptions.RefreshToken, tokenOptions.ExpiredTime);
                await _repositoryManager.AuthRepository.UpdateAsync(loginModel);

                return new UserAuthDto(new TokensDto(tokenOptions.AccessToken, tokenOptions.RefreshToken, _configuration.Issuer, _configuration.Audience), loginModel.Login, user.Roles);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
    }
}
