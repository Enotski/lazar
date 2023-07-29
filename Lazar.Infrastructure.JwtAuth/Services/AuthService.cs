using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Lazar.Infrastructure.JwtAuth.Services {
    public class AuthService : IAuthService {
        private readonly IRepositoryManager _repositoryManager;
        //private readonly IModelMapper _mapper;
        private readonly AuthOptions _configuration;
        public AuthService(IRepositoryManager repositoryManager, /*IModelMapper mapper,*/ IOptions<AuthOptions> options) {
            _repositoryManager = repositoryManager;
            //_mapper = mapper;
            _configuration = options.Value;
        }

        public TokensDto GenerateRefreshToken(string userName) {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public TokensDto GenerateToken(string userName) {
            var claims = new List<Claim>
{
            new Claim(type: ClaimTypes.Name, value: /*user.Login*/ "user123")
        };

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration.Key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
            throw new NotImplementedException();
        }
        public Task<UserAuthDto> Refresh(UserAuthDto model) {
            throw new NotImplementedException();
        }
        public async Task<UserAuthDto> LogInAsync(LogInRequestDto model) {


            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration.Key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new {
                access_token = encodedJwt,
                login = /*user.Login*/"user123"
            };
            try {
                if (model is null) {
                    throw new Exception("Invalid client request");
                }
                var user = await _repositoryManager.UserRepository.GetByLoginAsync(model.Login);
                if (user is null)
                    throw new Exception("User not found");

                if(user.Password != model.Password)
                    throw new Exception("Invalid password");

                var claims = new List<Claim>
                {
            new Claim(ClaimTypes.Name, loginModel.UserName),
            new Claim(ClaimTypes.Role, "Manager")
        };
                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _userContext.SaveChanges();
                return Ok(new AuthenticatedResponse {
                    Token = accessToken,
                    RefreshToken = refreshToken
                });
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
        public async Task<UserAuthDto> SignUpAsync(SignUpRequestDto model) {
            try {
                int totalRecords = await _repositoryManager.UserRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.UserRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<UserDto>(totalRecords, _mapper.Mapper.Map<IReadOnlyList<UserDto>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
        public async Task<bool> LogOutAsync(string login) {
            try {
                int totalRecords = await _repositoryManager.UserRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.UserRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<UserDto>(totalRecords, _mapper.Mapper.Map<IReadOnlyList<UserDto>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
    }
}
