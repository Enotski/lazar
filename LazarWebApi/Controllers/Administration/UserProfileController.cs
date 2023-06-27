using lazarData.Interfaces;
using lazarData.Models.Administration;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories;
using lazarData.Repositories.Administration;
using lazarData.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserProfileController : BaseController
    {
        public UserRepository userRepository;
        public UserProfileController(IContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }
        [HttpPost(Name = "getUserModel")]
        public JsonResult GetUserModel([FromBody] Guid? id)
        {
            var res = userRepository.GetView(id);
            return Json(res);
        }
        [HttpPost(Name = "updateUser")]
        public JsonResult AddEditUser([FromBody] UserViewModel model)
        {
            var data = userRepository.AddEditUser(model, CurrentUser.Id);
            return Json(data);
        }
        [HttpPost(Name = "setRoleToUser")]
        public JsonResult SetRoleToUser([FromBody] UserViewModel model)
        {
            var data = userRepository.SetRoleToUser(model.Id, model.RoleId/*, CurrentUser.Id*/);
            return Json(data);
        }
        [HttpGet(Name = "isUserAdmin")]
        public JsonResult IsUserAdmin()
        {
            var isAdmin = CurrentUser.Roles.Any(r => r.Id == Guids.Roles.Administrator);
            return Json(new BaseResponse(new BaseResponseModel(), new { UserIsAdmin = isAdmin }));
        }
    }
}