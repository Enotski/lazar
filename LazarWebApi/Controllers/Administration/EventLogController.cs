using lazarData.Interfaces;
using lazarData.Models.Response;
using lazarData.Models.Response.ViewModels;
using lazarData.Repositories.Administration;
using lazarData.ResponseModels.Dtos.Administration;
using lazarData.ResponseModels.Dx.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration
{
    /// <summary>
    /// Controller of events records in app
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventLogsController : BaseApiController
    {
        EventLogRepository eventLogRepository;
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="contextRepo">IContextRepository instance</param>
        public EventLogsController(IContextRepository contextRepo)
        {
            eventLogRepository = new EventLogRepository(contextRepo);
        }
        /// <summary>
        /// Get eventLogs list for dataGrid representation
        /// </summary>
        /// <param name="args">arguments from DxDataGrid (skip, take, sorts, filters)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetDataGridAsync([FromBody] DataGridRequestModel args)
        {
            var data = await eventLogRepository.GetEventLogsDataGridAsync(args.skip, args.take, args.sorts, args.filters);
            return Json(data);
        }
        /// <summary>
        /// Remove range of logs by date period
        /// </summary>
        /// <param name="period">period of dates (StartDate, EndDate)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveByPeriodAsync([FromBody] Period period)
        {
            if (DateTime.TryParse(period.startDate, out DateTime StartDate) && DateTime.TryParse(period.endDate, out DateTime EndDate))
            {
                var res = await eventLogRepository.RemoveLogsByPeriodAsync(StartDate, EndDate);
                return Json(res);
            } else { return Json(new EventLogDto()); }
        }
        /// <summary>
        /// Remove range of logs by keys
        /// </summary>
        /// <param name="ids">Array of keys</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveAsync([FromBody] Guid[] ids)
        {
            var res = await eventLogRepository.RemoveLogsAsync(ids, CurrentUser.Id);
            return Json(res);
        }
    }
}
