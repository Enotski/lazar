using Lazar.Srevices.Iterfaces.Administration;
using lazarData.Interfaces;
using lazarData.Repositories.Administration;
using lazarData.ResponseModels.Dtos.Administration;
using lazarData.ResponseModels.Dx;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public JsonResult GetDataGrid([FromBody] RoleDataGridRequest args) {
            var data = roleRepository.GetRolesDataGrid(args.skip, args.take, args.sorts, args.filters, args.selectedUserId);
            return Json(data);  
        }
        [HttpPost]
        [Route("get-list")]
        public JsonResult GetRoles([FromBody] DxSelectRoleRequest args)
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