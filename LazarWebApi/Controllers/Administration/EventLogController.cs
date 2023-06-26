using lazarData.Interfaces;
using lazarData.Models.Response;
using lazarData.Models.Response.DataGrid.Base;
using lazarData.Repositories;
using lazarData.Repositories.Administration;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventLogsController : BaseController
    {
        EventLogRepository eventLogRepository;
        public EventLogsController(IContextRepository contextRepo)
        {
            eventLogRepository = new EventLogRepository(contextRepo);
        }
        [HttpPost(Name = "getLog")]
        public JsonResult GetEventLogsDataGrid([FromBody] DataGridRequestModel args)
        {
            var data = eventLogRepository.GetEventLogsDataGrid(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
        }
        [HttpPost(Name = "removeLogByPeriod")]
        public JsonResult RemoveLogsByPeriod([FromBody] Period period)
        {
            var res = eventLogRepository.RemoveLogsByPeriod(period.StartDate, period.EndDate, CurrentUser);
            return Json(res);
        }
        [HttpPost(Name = "removeLogs")]
        public JsonResult RemoveLogs([FromBody] Guid[] ids)
        {
            var res = eventLogRepository.RemoveLogs(ids, CurrentUser.Id);
            return Json(res);
        }
    }
}
