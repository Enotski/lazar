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
    /// <summary>
    /// Controller of roles
    /// </summary>
    [ApiController]
    [Route("api/roles")/*, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
    public class RolesController : BaseController {
        public RolesController(IServiceManager serviceManager, IModelMapper mapper)
            : base(serviceManager, mapper) {
        }
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto options) {
            try {
                return Ok(await _serviceManager.RoleService.GetAsync(options));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-by-user")]
        public async Task<IActionResult> GetByUser([FromBody] RoleTableRequestDto options) {
            try {
                return Ok(await _serviceManager.RoleService.GetByUserAsync(options));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<IActionResult> GetRolesList([FromBody] KeyNameRequestDto options) {
            try {
                return Ok(await _serviceManager.RoleService.GetListAsync(options));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-list-by-user")]
        public async Task<IActionResult> GetRolesByUserList([FromBody] SelectRoleRequestDto options) {
            try {
                return Ok(await _serviceManager.RoleService.GetKeyNameByUserAsync(options));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetRoleModel([FromBody] Guid id) {
            try {
                return Ok(await _serviceManager.RoleService.GetAsync(id));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] RoleDto data) {
            try {
                await _serviceManager.RoleService.CreateAsync(data, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] RoleDto data) {
            try {
                await _serviceManager.RoleService.UpdateAsync(data, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] IEnumerable<Guid> ids) {
            try {
                await _serviceManager.RoleService.DeleteAsync(ids, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}