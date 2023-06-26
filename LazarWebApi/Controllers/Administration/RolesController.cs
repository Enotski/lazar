using lazarData.Interfaces;
using lazarData.Models.Response.DataGrid.Base;
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

        [HttpPost(Name = "getRoles")]
        public JsonResult GetRolesDataGrid([FromBody] DataGridRequestModel args) {
            var data = roleRepository.GetRolesDataGrid(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
        }
        [HttpPost(Name = "updateRole")]
        public JsonResult UpdateRole([FromBody] RoleViewModel model) {
            var data = roleRepository.UpdateRole(model.Id, model.Name, CurrentUser.Id);
            return Json(data);
        }
        [HttpPost(Name = "removeRoles")]
        public JsonResult DeleteRoles([FromBody] List<Guid> ListIdRoles) {
            var data = roleRepository.DeleteRoles(ListIdRoles, CurrentUser.Id);
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