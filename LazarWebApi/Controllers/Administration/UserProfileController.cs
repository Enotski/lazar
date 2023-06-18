using lazarData.Models.Administration;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using lazarData.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserProfileController : BaseController
    {
        public UserRepository userRepository = new UserRepository();
        [HttpPost(Name = "getUserModel")]
        public JsonResult GetUserModel([FromBody] Guid? id)
        {
            if (id.HasValue)
            {
                return Json(new BaseResponse(userRepository.GetViewById<User>(id.Value, true, x => x.Roles)));
            }
            return Json(new BaseResponse(new UserViewModel()));
        }
        [HttpPost(Name = "updateUser")]
        public JsonResult AddEditUser([FromBody] UserViewModel model)
        {
            var data = userRepository.AddEditUser(model, CurrentUser.Id);
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