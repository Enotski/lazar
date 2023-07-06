using lazarData.Interfaces;
using lazarData.Models.Response.DataGrid;
using lazarData.Models.Response.Dx;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : BaseApiController {

        RoleRepository roleRepository;
        public RolesController(IContextRepository contextRepo)
        {
            roleRepository = new RoleRepository(contextRepo);
        }

        [HttpPost]
        public JsonResult GetRolesDataGrid([FromBody] RoleDataGridRequest args) {
            var data = roleRepository.GetRolesDataGrid(args.skip, args.take, args.sorts, args.filters, args.selectedUserId);
            return Json(data);  
        }
        [HttpPost]
        public JsonResult GetRoles([FromBody] DxSelectRoleRequest args)
        {
            var data = roleRepository.GetRoles(args.searchValue ?? "", args.selectedUserId);
            return Json(data);
        }
        [HttpPost]
        public JsonResult AddRole([FromBody] RoleViewModel model)
        {
            var data = roleRepository.AddRole(model);
            return Json(data);
        }
        [HttpPost]
        public JsonResult UpdateRole([FromBody] RoleViewModel model) {
            var data = roleRepository.UpdateRole(model);
            return Json(data);
        }
        [HttpPost]
        public JsonResult DeleteRole([FromBody] RoleViewModel model) {
            var data = roleRepository.DeleteRole(model.Id);
            return Json(data);
        }
        [HttpGet]
        public JsonResult GetRoleModel(Guid? id = null)
        {
            var res = roleRepository.GetView(id);
            return Json(res);
        }
    }
}