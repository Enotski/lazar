using lazarData.Interfaces;
using lazarData.Models.Administration;
using lazarData.Repositories.Administration;
using LazarWebApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthenticationWithAngular.Controllers
{
    public class AuthController : BaseApiController
    {
        UserRepository userRepository;
        public AuthController(IContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            var user = userRepository.GetAll<User>(true).FirstOrDefault(x => x.Login == signInRequest.Login &&
                                                x.Password == signInRequest.Password);
            if (user is null)
            {
                return BadRequest();
            }

            var claims = new List<Claim>
        {
            new Claim(type: ClaimTypes.Name, value: user.Login)
        };
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                JwtBearerDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();

            return Ok(userClaims);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        }
    }
    record UserClaim(string Type, string Value);
    public record SignInRequest(string Login, string Password);
}