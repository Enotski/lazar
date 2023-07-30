using Lazar.Infrastructure.JwtAuth.Models;
using Lazar.Infrastructure.Mapper;
using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Services.Contracts.Response.Base;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lazar.Presentation.WebApi.Controllers.Auth {
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController {
        private readonly AuthOptions _configuration;
        public AuthController(IServiceManager serviceManager, IModelMapper mapper, IOptions<AuthOptions> options) : base(serviceManager, mapper) {
            _configuration = options.Value;
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequestDto signInRequest) {
            try {
                return Ok(await _serviceManager.AuthService.LogInAsync(signInRequest));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("log-out")]
        public async Task<IActionResult> LogOut([FromBody] string login) {
            try {
                await _serviceManager.AuthService.LogOutAsync(login);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpRequest) {
            try {
                return Ok(await _serviceManager.AuthService.SignUpAsync(signUpRequest));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("refresh")]
        public async Task <IActionResult> Refresh(TokensDto tokenApiModel) {
            try {
                return Ok(await _serviceManager.AuthService.RefreshTokenAsync(tokenApiModel));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}