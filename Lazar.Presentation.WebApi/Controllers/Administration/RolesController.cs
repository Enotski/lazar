using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration {
    /// <summary>
    /// Controller of roles
    /// </summary>
    [ApiController]
    [Route("api/roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : BaseController {
        public RolesController(IServiceManager serviceManager)
            : base(serviceManager) {
        }
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto search) {
            try {
                return Ok(await _serviceManager.RoleService.GetAsync(search));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-list")]
        public JsonResult GetRoles([FromBody] SelectRoleRequestDto args)
        {
            var data = roleRepository.GetRoles(args.SearchValue ?? "", args.SelectedUserId);
            return Json(data);
        }
        [HttpPost]
        [Route("create")]
        public JsonResult AddRole([FromBody] RoleDto model)
        {
            var data = roleRepository.AddRole(model);
            return Json(data);
        }
        [HttpPost]
        [Route("update")]
        public JsonResult UpdateRole([FromBody] RoleDto model) {
            var data = roleRepository.UpdateRole(model);
            return Json(data);
        }
        [HttpPost]
        [Route("delete")]
        public JsonResult DeleteRole([FromBody] RoleDto model) {
            var data = roleRepository.DeleteRole(model.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("get/{id?}")]
        public JsonResult GetRoleModel(Guid? id = null)
        {
            var res = roleRepository.GetView(id);
            return Json(res);
        }
    }
}