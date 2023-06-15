using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private static readonly string[] Summaries = new[]
//        {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//        private readonly ILogger<WeatherForecastController> _logger;

//        public WeatherForecastController(ILogger<WeatherForecastController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateTime.Now.AddDays(index),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}

/// <summary>
/// Контроллер логирования событий
/// </summary>
public class EventLogsController : BaseController {
	protected override string _pathToView => "~/Views/Logs/EventLogs.cshtml";
	EventLogRepository eventLogRepository = new EventLogRepository();

	public JsonResult GetEventLogsDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, Guid? filial) {
		var data = eventLogRepository.GetEventLogsDataGrid(skip, take, sorts, filters, filial);
		return Json(data, JsonRequestBehavior.AllowGet);
	}

	public JsonResult RemoveLogsByPeriod(DateTime startDate, DateTime endDate, Guid? filialId, bool allFilials) {
		var res = eventLogRepository.RemoveLogsByPeriod(startDate, endDate, filialId, allFilials, CurrentUser.Roles);
		return Json(res);
	}

	public JsonResult RemoveLogs(Guid[] ids, Guid filialId) {
		var res = eventLogRepository.RemoveLogs(ids, filialId, CurrentUser.Roles);
		return Json(res);
	}
}