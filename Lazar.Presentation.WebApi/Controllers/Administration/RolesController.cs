using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.Repositories.Administration;
using Lazar.Domain.Core.ResponseModels.Dtos.Administration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lazar.Services.Contracts.Request.DataGrid;
using Lazar.Services.Contracts.Request;

namespace LazarWebApi.Controllers.Administration
{
    /// <summary>
    /// Controller of roles
    /// </summary>
    [ApiController]
    [Route("api/roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController {

        IRoleService _roleService;
        public RolesController(IRoleService service)
        {
            _roleService = service;
        }
        /// <summary>
        /// Get data grid
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-grid")]
        public JsonResult GetDataGrid([FromBody] RoleDataGridRequestDto args) {
            var data = roleRepository.GetRolesDataGrid(args.skip, args.take, args.sorts, args.filters, args.selectedUserId);
            return Json(data);  
        }
        [HttpPost]
        [Route("get-list")]
        public JsonResult GetRoles([FromBody] SelectRoleRequestDto args)
        {
            var data = roleRepository.GetRoles(args.searchValue ?? "", args.selectedUserId);
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