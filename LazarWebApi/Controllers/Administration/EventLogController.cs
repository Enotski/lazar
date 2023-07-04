using lazarData.Interfaces;
using lazarData.Models.Response;
using lazarData.Models.Response.DataGrid.Base;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    public class EventLogsController : BaseApiController
    {
        EventLogRepository eventLogRepository;
        public EventLogsController(IContextRepository contextRepo)
        {
            eventLogRepository = new EventLogRepository(contextRepo);
        }
        [HttpPost]
        public JsonResult GetEventLogsDataGrid([FromBody] DataGridRequestModel args)
        {
            var data = eventLogRepository.GetEventLogsDataGrid(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
        }
        [HttpPost]
        public JsonResult RemoveLogsByPeriod([FromBody] Period period)
        {
            if(DateTime.TryParse(period.startDate, out DateTime StartDate) && DateTime.TryParse(period.endDate, out DateTime EndDate)) {
                var res = eventLogRepository.RemoveLogsByPeriod(StartDate, EndDate);
                return Json(res);
            }else { return Json(new EventLogViewModel()); }
        }
        [HttpPost]
        public JsonResult RemoveLogs([FromBody] Guid[] ids)
        {
            var res = eventLogRepository.RemoveLogs(ids, CurrentUser.Id);
            return Json(res);
        }
    }
}
