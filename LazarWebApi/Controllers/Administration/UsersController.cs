using Microsoft.AspNetCore.Mvc;

//namespace LazarWebApi.Controllers.Administration {
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase {
//        private static readonly string[] Summaries = new[]
//        {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };

//        private readonly ILogger<WeatherForecastController> _logger;

//        public WeatherForecastController(ILogger<WeatherForecastController> logger) {
//            _logger = logger;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get() {
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
//                Date = DateTime.Now.AddDays(index),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}
 using SysRM.Data.Repositories;
 using SysRM.Entities.Views.Response;
 using SysRM.Entities.Views.ViewModels;
 using System;
 using System.Collections.Generic;
 using System.Web.Mvc;
 using TMK.Utils.Enums;

 namespace LazarWebApi.Controllers.Administration {
    public class UsersController : BaseController {
        protected override string _pathToView => "~/Views/Administration/Users.cshtml";
        UserRepository userRepository = new UserRepository();

        public JsonResult GetUsersDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
            var data = userRepository.GetUsersDataGrid(skip, take, sorts, filters);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUsers(IEnumerable<Guid> userIds) {
            var data = userRepository.DeleteUsers(userIds);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}