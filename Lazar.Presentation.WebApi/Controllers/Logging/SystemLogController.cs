using Lazar.Infrastructure.Mapper;
using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Services.Contracts.Request;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lazar.Presentation.WebApi.Controllers.Logging {
    [ApiController]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SystemLogController : BaseController {
        public SystemLogController(IServiceManager serviceManager, IModelMapper mapper)
            : base(serviceManager, mapper) {
        }
        /// <summary>
        /// Get Logging list for dataGrid representation
        /// </summary>
        /// <param name="args">arguments from DxDataGrid (skip, take, sorts, filters)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetDataGridAsync([FromBody] DataGridRequestDto args)
        {
            var data = await LoggingRepository.GetLoggingDataGridAsync(args.skip, args.take, args.Sorts, args.Filters);
            return Json(data);
        }
        /// <summary>
        /// Remove range of logs by date period
        /// </summary>
        /// <param name="period">period of dates (StartDate, EndDate)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveByPeriodAsync([FromBody] PeriodDto period)
        {
            if (DateTime.TryParse(period.startDate, out DateTime StartDate) && DateTime.TryParse(period.endDate, out DateTime EndDate))
            {
                var res = await LoggingRepository.RemoveLogsByPeriodAsync(StartDate, EndDate);
                return Json(res);
            }
            else { return Json(new LoggingDto()); }
        }
        /// <summary>
        /// Remove range of logs by keys
        /// </summary>
        /// <param name="ids">Array of keys</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveAsync([FromBody] Guid[] ids)
        {
            var res = await LoggingRepository.RemoveLogsAsync(ids, CurrentUser.Id);
            return Json(res);
        }
    }
}
