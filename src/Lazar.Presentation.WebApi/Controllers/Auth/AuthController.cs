using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Lazar.Infrastructure.JwtAuth.Models.Dto;
using Lazar.Services.Contracts.Response.Base;
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
        [HttpPost]
        [Route("log-out")]
        public async Task<IActionResult> LogOut([FromBody] LogOutRequestDto model) {
            try {
                await _authService.LogOutAsync(model);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserRegisterRequestDto signUpRequest) {
            try {
                return Ok(await _authService.RegisterAsync(signUpRequest));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("refresh")]
        public async Task <IActionResult> Refresh([FromBody] TokensDto tokenApiModel) {
            try {
                return Ok(await _authService.RefreshTokenAsync(tokenApiModel));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}