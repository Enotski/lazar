using lazarData.Interfaces;
using lazarData.Repositories.Administration;
using LazarWebApi.Controllers;

namespace CookieAuthenticationWithAngular.Controllers
{
    public class AuthController : BaseApiController
    {
        UserRepository userRepository;
        public AuthController(IContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }

        //[HttpPost("signin")]
        //public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        //{
        //    var user = users.FirstOrDefault(x => x.Email == signInRequest.Email &&
        //                                        x.Password == signInRequest.Password);
        //    if (user is null)
        //    {
        //        return BadRequest(new Response(false, "Invalid credentials."));
        //    }

        //    var claims = new List<Claim>
        //{
        //    new Claim(type: ClaimTypes.Email, value: signInRequest.Email),
        //    new Claim(type: ClaimTypes.Name,value: user.Name)
        //};
        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(identity),
        //        new AuthenticationProperties
        //        {
        //            IsPersistent = true,
        //            AllowRefresh = true,
        //            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        //        });

        //    return Ok(new Response(true, "Signed in successfully"));
        //}

        //[Authorize]
        //[HttpGet("user")]
        //public IActionResult GetUser()
        //{
        //    var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();

        //    return Ok(userClaims);
        //}

        //[Authorize]
        //[HttpGet("signout")]
        //public async Task SignOutAsync()
        //{
        //    await HttpContext.SignOutAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme);
        //}
    }
    record UserClaim(string Type, string Value);
    record SignInRequest(string Email, string Password);
}