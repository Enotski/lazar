using Lazar.Infrastructure.Mapper;
using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Base;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration {
    [ApiController]
    [Route("api/users")/*, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
    public class UsersController : BaseController {
        public UsersController(IServiceManager serviceManager, IModelMapper mapper)
            : base(serviceManager, mapper) {
        }
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto options) {
            try {
                return Ok(await _serviceManager.UsersService.GetAsync(options));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<IActionResult> GetUsersList([FromBody] KeyNameRequestDto options) {
            try {
                return Ok(await _serviceManager.UsersService.GetListAsync(options));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get/{id}")]
        public async Task<IActionResult> GetModel([FromBody] Guid id) {
            try {
                return Ok(await _serviceManager.UsersService.GetAsync(id));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserDto data) {
            try {
                await _serviceManager.UsersService.CreateAsync(data, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UserDto data) {
            try {
                await _serviceManager.UsersService.UpdateAsync(data, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids) {
            try {
                await _serviceManager.RoleService.DeleteAsync(ids, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}