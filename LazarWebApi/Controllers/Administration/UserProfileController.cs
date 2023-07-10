using lazarData.Interfaces;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using lazarData.ResponseModels.Dtos.Administration;
using lazarData.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public JsonResult AddEditUser([FromBody] UserDto model)
        {
            var data = userRepository.AddEditUser(model, CurrentUser.Id);
            return Json(data);
        }
        [HttpPost]
        public JsonResult SetRoleToUser([FromBody] UserDto model)
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
        public JsonResult LoginUser(UserDto model)
        {
            return Json(userRepository.LoginUser(model));
        }
        [HttpPost]
        public JsonResult RegisterUser(UserDto model)
        {
            return Json(userRepository.AddUser(model));
        }
    }
}