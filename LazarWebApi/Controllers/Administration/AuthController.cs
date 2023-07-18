using lazarData.Auth;
using lazarData.Interfaces;
using lazarData.Models.Administration;
using lazarData.Repositories.Administration;
using LazarWebApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookieAuthenticationWithAngular.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly AuthOptions _configuration;

        private readonly UserRepository userRepository;
        public AuthController(IOptions<AuthOptions> options, IContextRepository contextRepo)
        {
            _configuration = options.Value;
            userRepository = new UserRepository(contextRepo);
        }

        [HttpPost]
        public IActionResult LogInAsync([FromBody] SignInRequest signInRequest)
        {
            //var user = userRepository.GetAll<User>(true).FirstOrDefault(x => x.Login == signInRequest.Login
            //&& x.Password == signInRequest.Password);
            //if (user is null)
            //{
            //    return BadRequest();
            //}

            var claims = new List<Claim>
        {
            new Claim(type: ClaimTypes.Name, value: /*user.Login*/ "user123")
        };
            //var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration.Key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                login = /*user.Login*/"user123"
            };

            return Json(response);
        }
        [HttpPost]
        public IActionResult SignUpAsync([FromBody] SignUpRequest signUpRequest)
        {
            //var user = userRepository.GetAll<User>(true).FirstOrDefault(x => x.Login == signInRequest.Login
            //&& x.Password == signInRequest.Password);
            //if (user is null)
            //{
            //    return BadRequest();
            //}

            var claims = new List<Claim>
        {
            new Claim(type: ClaimTypes.Name, value: /*user.Login*/ "user123")
        };
            //var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration.Key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                login = /*user.Login*/"user123"
            };

            return Json(response);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult GetUser()
        {
            var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();

            return Ok(userClaims);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        }
    }
    record UserClaim(string Type, string Value);
    public record SignInRequest(string Login, string Password);
    public record SignUpRequest(string Login, string Email, string Password);
}