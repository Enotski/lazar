using lazarData.Models.Response.DataGrid.Base;
using lazarData.Repositories;
using lazarData.Repositories.Administration;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : BaseController {
        UserRepository userRepository;
        public UsersController(ContextRepository contextRepo)
        {
            userRepository = new UserRepository(contextRepo);
        }
        [HttpPost(Name = "getUsersDataGrid")]
        public JsonResult GetUsersDataGrid([FromBody] DataGridRequestModel args)
        {
            var data = userRepository.GetUsersDataGrid(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
        }
        [HttpPost(Name = "removeUsers")]
        public JsonResult DeleteUsers([FromBody] IEnumerable<Guid> userIds)
        {
            var data = userRepository.DeleteUsers(userIds);
            return Json(data);
        }
    }
}