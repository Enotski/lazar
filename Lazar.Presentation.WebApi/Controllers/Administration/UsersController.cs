using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.Repositories.Administration;
using Lazar.Domain.Core.ResponseModels.Dtos.Administration;
using Lazar.Services.Contracts.Request.DataGrid.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseApiController {
        UserRepository userRepository;
        public UsersController(IContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }
        [HttpPost]
        public JsonResult GetUsersDataGrid([FromBody] DataGridRequestDto args)
        {
            var data = userRepository.GetUsersDataGrid(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
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