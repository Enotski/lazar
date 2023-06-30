using lazarData.Interfaces;
using lazarData.Models.Response.DataGrid;
using lazarData.Models.Response.Dx;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RolesController : BaseController {

        RoleRepository roleRepository;
        public RolesController(IContextRepository contextRepo)
        {
            roleRepository = new RoleRepository(contextRepo);
        }

        [HttpPost(Name = "getRolesGrid")]
        public JsonResult GetRolesDataGrid([FromBody] RoleDataGridRequest args) {
            var data = roleRepository.GetRolesDataGrid(args.skip, args.take, args.sorts, args.filters, args.selectedUserId);
            return Json(data);
        }
        [HttpPost(Name = "getRoles")]
        public JsonResult GetRoles([FromBody] DxSelectRoleRequest args)
        {
            var data = roleRepository.GetRoles(args.searchValue ?? "", args.selectedUserId);
            return Json(data);
        }
        [HttpPost(Name = "addRole")]
        public JsonResult AddRole([FromBody] RoleViewModel model)
        {
            var data = roleRepository.AddRole(model);
            return Json(data);
        }
        [HttpPost(Name = "updateRole")]
        public JsonResult UpdateRole([FromBody] RoleViewModel model) {
            var data = roleRepository.UpdateRole(model);
            return Json(data);
        }
        [HttpPost(Name = "deleteRole")]
        public JsonResult DeleteRole([FromBody] RoleViewModel model) {
            var data = roleRepository.DeleteRole(model.Id);
            return Json(data);
        }
        [HttpGet(Name = "getRoleModel")]
        public JsonResult GetRoleModel(Guid? id = null)
        {
            var res = roleRepository.GetView(id);
            return Json(res);
        }
    }
}