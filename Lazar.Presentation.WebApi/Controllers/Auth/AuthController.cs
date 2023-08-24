using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Models;
using Lazar.Services.Contracts.Response.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lazar.Presentation.WebApi.Controllers.Auth {
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequestDto signInRequest) {
            try {
                return Ok(await _authService.LogInAsync(signInRequest));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("log-out")]
        public async Task<IActionResult> LogOut([FromBody] string login) {
            try {
                await _authService.LogOutAsync(login);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpRequest) {
            try {
                return Ok(await _authService.SignUpAsync(signUpRequest));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("refresh")]
        public async Task <IActionResult> Refresh(TokensDto tokenApiModel) {
            try {
                return Ok(await _authService.RefreshTokenAsync(tokenApiModel));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}