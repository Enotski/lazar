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
// using SysRM.Data.Repositories;
// using SysRM.Entities.Views.Response;
// using SysRM.Entities.Views.ViewModels.User;
// using System;
// using System.Web.Mvc;

// namespace SysRM.Controllers.User {
//     public class UserProfileController : BaseController {
// 		protected override string _pathToView => "~/Views/User/UserProfile.cshtml";
// 		public UserRepository userRepository = new UserRepository();

// 		public JsonResult ChangeNotificationSettings(UserProfileNotificationSettings notificationSettings) {
// 			var data = userRepository.ChangeNotificationSettings(notificationSettings, CurrentUser?.UserInfo?.UserId);
// 			return Json(data);
// 		}
// 		[HttpPost]
// 		public JsonResult GetIndicatorsTableGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
// 			var data = userRepository.GetIndicatorsTableGrid(skip, take, sorts, filters, CurrentUser.UserInfo);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}
// 		public JsonResult GetIndicatorModel(Guid? id) {
// 			return Json(userRepository.GetIndicatorModel(id.Value, CurrentUser?.UserInfo.UserId));
// 		}
// 		/// <summary>
// 		/// Установка пользовательской даты подачи заявки для показателя
// 		/// </summary>
// 		/// <param name="id"></param>
// 		/// <param name="deadline"></param>
// 		/// <returns></returns>
// 		public JsonResult SetUserDeadlineForSubmitting(Guid id, DateTime? deadline, int type, int month, int day) {
// 			var data = userRepository.SetUserDeadlineForSubmitting(id, deadline, type, month, day, CurrentUser?.UserInfo?.UserId);
// 			return Json(data);
// 		}
// 		public JsonResult RemoveTableStorageSettings() {
// 			var data = userRepository.RemoveTableStorageSettings(CurrentUser?.UserInfo?.UserId);
// 			return Json(data);
// 		}
// 	}
// }