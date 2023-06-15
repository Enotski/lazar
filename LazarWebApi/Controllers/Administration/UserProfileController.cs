// using Microsoft.AspNetCore.Mvc;

// namespace LazarWebApi.Controllers.Administration
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class WeatherForecastController : ControllerBase
//     {
//         private static readonly string[] Summaries = new[]
//         {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };

//         private readonly ILogger<WeatherForecastController> _logger;

//         public WeatherForecastController(ILogger<WeatherForecastController> logger)
//         {
//             _logger = logger;
//         }

//         [HttpGet(Name = "GetWeatherForecast")]
//         public IEnumerable<WeatherForecast> Get()
//         {
//             return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//             {
//                 Date = DateTime.Now.AddDays(index),
//                 TemperatureC = Random.Shared.Next(-20, 55),
//                 Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//             })
//             .ToArray();
//         }
//     }
// }
using SysRM.Data.Repositories;
using SysRM.Entities.Views.Response;
using SysRM.Entities.Views.ViewModels.User;
using System;
using System.Web.Mvc;

namespace SysRM.Controllers.User {
    public class UserProfileController : BaseController {
        protected override string _pathToView => "~/Views/User/UserProfile.cshtml";
        public UserRepository userRepository = new UserRepository();
        public JsonResult GetUserModel(Guid? id) {
            if (id.HasValue) {
                return Json(new BaseResponse(userRepository.GetViewById<Entities.Models.References.Administration.User>(id.Value,
                    DeletedSearchType.Actual,
                    true,
                    x => x.Department,
                    x => x.Post,
                    x => x.Filial,
                    x => x.Roles)));
            }
            return Json(new BaseResponse(new UserViewModel {
                Id = null,
                Name = "",
                Surname = "",
                Patronymic = "",
                Login = "",
                Email = "",
                DepartmentName = "",
                PostName = "",
                FilialName = "",
                DepartmentId = null,
                PostId = null,
                FilialId = CurrentUser?.UserInfo.FilialId ?? Guid.Empty
            }));
        }
        public JsonResult AddEditUser(Guid? userId, Guid filialId/*, Guid? departmentId, Guid? postId*/, string userLogin, string userEmail, string userName, string userSurname, string userPatronymic) {
            var data = userRepository.AddEditUser(userId, filialId, null, null, null, userLogin, userEmail, userName, userSurname, userPatronymic, CurrentUser?.UserInfo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}