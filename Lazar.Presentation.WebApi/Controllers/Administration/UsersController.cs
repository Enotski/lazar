using Lazar.Infrastructure.Mapper;
using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration {
    [ApiController]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseController {
        public UsersController(IServiceManager serviceManager, IModelMapper mapper)
            : base(serviceManager, mapper) {
        }
        [HttpPost]
        public JsonResult GetUsersDataGrid([FromBody] DataGridRequestDto args)
        {
            var data = userRepository.GetUsersDataGrid(args.skip, args.take, args.Sorts, args.Filters);
            return Json(data); [HttpPost]
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
        [HttpPost]
        public JsonResult AddUser([FromBody] UserDto model)
        {
            var data = userRepository.AddUser(model);
            return Json(data);
        }
        [HttpPost]
        public JsonResult UpdateUser([FromBody] UserDto model)
        {
            var data = userRepository.UpdateUser(model);
            return Json(data);
        }
        [HttpPost]
        public JsonResult RemoveRoleFromUser([FromBody] UserDto model)
        {
            var data = userRepository.RemoveRoleFromUser(model.Id, model.RoleId);
            return Json(data);
        }
        [HttpPost]
        public JsonResult DeleteUser([FromBody] UserDto model)
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