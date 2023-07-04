using lazarData.Interfaces;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using lazarData.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    public class UserProfileController : BaseApiController
    {
        public UserRepository userRepository;
        public UserProfileController(IContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }
        [HttpPost]
        public JsonResult GetUserModel([FromBody] Guid? id)
        {
            var res = userRepository.GetView(id);
            return Json(res);
        }
        [HttpPost]
        public JsonResult AddEditUser([FromBody] UserViewModel model)
        {
            var data = userRepository.AddEditUser(model, CurrentUser.Id);
            return Json(data);
        }
        [HttpPost]
        public JsonResult SetRoleToUser([FromBody] UserViewModel model)
        {
            var data = userRepository.SetRoleToUser(model.Id, model.RoleId/*, CurrentUser.Id*/);
            return Json(data);
        }
        [HttpGet]
        public JsonResult IsUserAdmin()
        {
            var isAdmin = CurrentUser.Roles.Any(r => r.Id == Guids.Roles.Administrator);
            return Json(new BaseResponse(new BaseResponseModel(), new { UserIsAdmin = isAdmin }));
        }
        [HttpPost]
        public JsonResult LoginUser(UserViewModel model)
        {
            return Json(userRepository.LoginUser(model));
        }
        [HttpPost]
        public JsonResult RegisterUser(UserViewModel model)
        {
            return Json(userRepository.AddUser(model));
        }
    }
}