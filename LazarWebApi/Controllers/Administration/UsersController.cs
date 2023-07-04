using lazarData.Interfaces;
using lazarData.Models.Response.DataGrid.Base;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    public class UsersController : BaseApiController {
        UserRepository userRepository;
        public UsersController(IContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }
        [HttpPost]
        public JsonResult GetUsersDataGrid([FromBody] DataGridRequestModel args)
        {
            var data = userRepository.GetUsersDataGrid(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
        }
        [HttpPost]
        public JsonResult AddUser([FromBody] UserViewModel model)
        {
            var data = userRepository.AddUser(model);
            return Json(data);
        }
        [HttpPost]
        public JsonResult UpdateUser([FromBody] UserViewModel model)
        {
            var data = userRepository.UpdateUser(model);
            return Json(data);
        }
        [HttpPost]
        public JsonResult RemoveRoleFromUser([FromBody] UserViewModel model)
        {
            var data = userRepository.RemoveRoleFromUser(model.Id, model.RoleId);
            return Json(data);
        }
        [HttpPost]
        public JsonResult DeleteUser([FromBody] UserViewModel model)
        {
            var data = userRepository.DeleteUser(model.Id);
            return Json(data);
        }
        [HttpPost]
        public JsonResult DeleteUsers([FromBody] IEnumerable<Guid> userIds)
        {
            var data = userRepository.DeleteUsers(userIds);
            return Json(data);
        }
    }
}